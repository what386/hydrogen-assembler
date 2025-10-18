namespace Assembler.Core;

using System.Text.RegularExpressions;

using Assembler.Exceptions;

public class Normalizer
{
    private Dictionary<string, int> labels = new();
    
    public string[] Normalize(string[] lines)
    {
        lines = RemoveComments(lines);
        lines = ProcessLabels(lines);
        lines = AlignPages(lines);
        return lines;
    }
    
    public string[] RemoveComments(string[] lines)
    {
        var result = new List<string>(lines.Length);
        
        foreach (var line in lines)
        {
            int commentIndex = line.IndexOf(';');
            string cleaned = commentIndex >= 0 
                ? line.Substring(0, commentIndex) 
                : line;
            cleaned = cleaned.Trim();
            
            if (!string.IsNullOrWhiteSpace(cleaned))
                result.Add(cleaned);
        }
        
        return result.ToArray();
    }
    
    private string[] ProcessLabels(string[] lines)
    {
        labels.Clear();
        var result = new List<string>();
        int lineNumber = 0;
        
        // First pass: find all labels
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            if (line.EndsWith(':'))
            {
                if (line.StartsWith("PAGE"))
                {
                    result.Add(line);
                    continue;
                }
                string labelName = line.TrimEnd(':');
                labels[labelName] = lineNumber;
                continue;
            }
            
            lineNumber++;
        }
        
        // Second pass: replace label references
        result.Clear();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.EndsWith(':'))
                continue;
            
            result.Add(ReplaceLabelReferences(line));
        }
        
        return result.ToArray();
    }
    
    private string ReplaceLabelReferences(string line)
    {
        foreach (var kvp in labels.OrderByDescending(x => x.Key.Length))
        {
            string pattern = @"\." + Regex.Escape(kvp.Key) + @"\b";
            line = Regex.Replace(line, pattern, "!" + kvp.Value.ToString());
        }
        return line;
    }
    
    private string[] AlignPages(string[] lines)
    {
        const int PageSize = 64;
        
        if (lines.Length == 0 || !lines[0].StartsWith("PAGE0"))
            throw new FormatException("Program must begin with PAGE0:");
        
        var result = new List<string>();
        int currentPageLine = 0;
        string currentPageName = "PAGE0";
        
        foreach (var line in lines)
        {
            if (line.StartsWith("PAGE", StringComparison.OrdinalIgnoreCase))
            {
                if (currentPageLine > 0)
                {
                    if (currentPageLine > PageSize)
                        throw new FormatException($"{currentPageName} exceeds {PageSize} instructions.");
                    
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
            throw new FormatException($"{currentPageName} exceeds {PageSize} instructions.");
        
        return result.ToArray();
    }
}
