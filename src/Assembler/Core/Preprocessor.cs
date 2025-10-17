namespace Assembler.Core;

using System.Text.RegularExpressions;

public class Preprocessor
{
    private Dictionary<string, string> definitions = new();
    private Dictionary<string, int> labels = new();

    private Stack<bool> conditionalStack = new();
    private bool IsActive() => conditionalStack.Count == 0 || conditionalStack.All(x => x);

    private HashSet<string> includedFiles = new();
    private List<string[]> pendingIncludes = new(); 
    
    public string[] PreprocessFile(string[] lines, string includedir)
    {
        lines = RemoveComments(lines);
        lines = ProcessDirectives(lines, includedir);
        lines = AppendIncludes(lines);
        lines = ProcessLabels(lines);
        lines = AlignPages(lines);
        
        return lines;
    }
   
    private string[] RemoveComments(string[] lines)
    {
        var result = new List<string>(lines.Length);
        
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            
            int commentIndex = line.IndexOf(';');
            
            if (commentIndex >= 0)
                line = line.Substring(0, commentIndex);

            line = line.Trim();
            
            if (!string.IsNullOrWhiteSpace(line))
                result.Add(line);
        }
        
        return result.ToArray();
    }

    private string[] ProcessDirectives(string[] lines, string basePath)
    {
        var result = new List<string>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (!line.StartsWith("#") && IsActive())
            {
                if(IsActive())
                {
                    string processed = ReplaceDefinitions(line);
                    result.Add(processed);
                }
                
                continue;
            }

            string directive = GetDirectiveName(line);
            
            switch (directive)
            {
                case "define":
                {
                    if (!IsActive()) break;

                    var parts = line.Substring(8).Split(new[]{' '}, 2);
                    if (parts.Length == 2)
                        definitions[parts[0]] = parts[1];

                    break;
                }
                
                case "undef":
                {
                    if (!IsActive()) break;

                    string symbol = line.Substring(7).Trim();
                    definitions.Remove(symbol);

                    break;
                }
                    
                case "include": // Collects includes but doesnt insert them
                {
                    if (!IsActive()) break;

                    var file = line.Substring(9).Trim().Trim('"');
                    var fullPath = Path.Combine(basePath, file);
                   
                    // Prevent cyclic dependencies
                    if (!includedFiles.Contains(fullPath))
                    {
                        includedFiles.Add(fullPath);
                       
                        // Dont process labels or pages yet
                        string[] includedFileLines = File.ReadAllLines(fullPath);

                        includedFileLines = RemoveComments(includedFileLines);
                        includedFileLines = ProcessDirectives(includedFileLines, Path.GetDirectoryName(fullPath));
                        
                        pendingIncludes.Add(includedFileLines);
                    }
                    break;
                }
                    
                case "ifdef":
                {
                    string symbol = line.Substring(7).Trim();
                    conditionalStack.Push(IsActive() && definitions.ContainsKey(symbol));
                    break;
                }
                        
                case "ifndef":
                {
                    string symbol = line.Substring(8).Trim();
                    conditionalStack.Push(IsActive() && !definitions.ContainsKey(symbol));
                    break;
                }
                        
                case "if":
                {
                    string condition = line.Substring(4).Trim();
                    conditionalStack.Push(IsActive() && EvaluateCondition(condition));
                    break;
                }
                                        
                case "else":
                {
                    if(conditionalStack.Count < 1)
                        throw new ArgumentException("Unable to end null statement");

                    bool current = conditionalStack.Pop();
                    bool parentActive = conditionalStack.Count == 0 || conditionalStack.All(x => x);
                    conditionalStack.Push(parentActive && !current);
                    break;
                }
                    
                case "end":
                {
                    if(conditionalStack.Count < 1)
                        throw new ArgumentException("Unable to end null statement");

                    conditionalStack.Pop();
                    break;
                }

                default:
                    throw new ArgumentException($"Unknown directive: #{directive}");
            }
        }

        return result.ToArray();
    }

    private string GetDirectiveName(string line)
    {
        // Extract directive name from "#directivename ..."
        // e.g., "#define X 5" -> "define"
        //       "#ifdef X" -> "ifdef"
        
        int spaceIndex = line.IndexOf(' ');
        if (spaceIndex < 0)
        {
            // No space means directive with no arguments (like #else, #end)
            return line.Substring(1);  // Remove '#'
        }
        
        return line.Substring(1, spaceIndex - 1);
    }
    
    private string[] AppendIncludes(string[] mainLines)
    {
        if (pendingIncludes.Count == 0)
            return mainLines;
        
        var result = new List<string>(mainLines);
        int nextPageNumber = CalculateNextPageNumber(mainLines);
        
        // Append each include with its own page
        foreach (var include in pendingIncludes)
        {
            result.Add($"PAGE{nextPageNumber}:");
            
            result.AddRange(include);
            
            nextPageNumber++;
        }
        
        // Clear for next preprocessing run
        pendingIncludes.Clear();
        
        return result.ToArray();
    }
    
    private int CalculateNextPageNumber(string[] lines)
    {
        int maxPage = -1;
        
        foreach (var line in lines)
        {
            if (line.StartsWith("PAGE"))
            {
                // Extract page number from "PAGE0:", "PAGE1:", etc.
                string pageStr = line.Substring(5).TrimEnd(':');
                if (int.TryParse(pageStr, out int pageNum))
                    maxPage = Math.Max(maxPage, pageNum);
            }
        }
        
        return maxPage + 1;
    }
    
    private static string[] AlignPages(string[] lines)
    {
        const int PageSize = 64;
        var result = new List<string>();
        int currentPageLine = 0;
        string currentPageName = "PAGE0";

        if (lines.Length == 0 || !lines[0].StartsWith("PAGE0"))
            throw new InvalidOperationException("Program must begin with PAGE0:");

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.StartsWith("PAGE", StringComparison.OrdinalIgnoreCase))
            {
                if (currentPageLine > 0)
                {
                    if (currentPageLine > PageSize)
                        throw new InvalidOperationException($"{currentPageName} exceeds {PageSize} instructions.");
                    
                    // Pad until page size is 64
                    while (currentPageLine % PageSize != 0)
                    {
                        result.Add("nop");
                        currentPageLine++;
                    }
                }

                currentPageName = line.TrimEnd(':');
                result.Add(line);
                currentPageLine = 0;
                continue;
            }

            result.Add(line);
            currentPageLine++;
        }

        if (currentPageLine > PageSize)
            throw new InvalidOperationException($"{currentPageName} exceeds {PageSize} instructions.");

        return result.ToArray();
    }
    
        
    
    private string[] ProcessLabels(string[] lines)
    {
        var result = new List<string>();
        int lineNumber = 0;
        
        // First pass: find all labels and build table
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            if (line.EndsWith(':'))
            {
                // Skip page markers, but keep in output
                if (line.StartsWith("PAGE"))
                {
                    result.Add(line);
                    continue;
                }

                string labelName = line.TrimEnd(':');
                labels[labelName] = lineNumber;
                // Only real instructions increment counter
                continue;
            }
            
            lineNumber++;
        }
        
        // Second pass: replace label references and output
        lineNumber = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            // Skip label definitions
            if (line.EndsWith(':'))
                continue;
            
            line = ReplaceLabelReferences(line);
            
            result.Add(line);
            lineNumber++;
        }
        
        return result.ToArray();
    }
    
    private string ReplaceLabelReferences(string line)
    {
        // Replace .labelname with !linenum (immediate format)
        foreach (var kvp in labels.OrderByDescending(x => x.Key.Length))
        {
            string pattern = @"\." + Regex.Escape(kvp.Key) + @"\b";
            line = Regex.Replace(line, pattern, "!" + kvp.Value.ToString());
        }
        return line;
    } 

    private string ReplaceDefinitions(string line)
    {
        string result = line;
        foreach (var kvp in definitions)
        {
            // Use word boundaries to match whole words only
            string pattern = @"\b" + Regex.Escape(kvp.Key) + @"\b";
            result = Regex.Replace(result, pattern, kvp.Value);
        }
        return result;
    }
    
    private bool EvaluateCondition(string condition)
    {
        foreach (var kvp in definitions)
        {
            condition = condition.Replace(kvp.Key, kvp.Value);
        }
        
        try
        {
            var dt = new System.Data.DataTable();
            var result = dt.Compute(condition, "");
            return Convert.ToBoolean(result);
        }
        catch
        {
            return false;
        }
    }
}
