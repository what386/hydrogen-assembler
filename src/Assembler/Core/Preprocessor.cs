namespace Assembler.Core;

using System.Text.RegularExpressions;
using Models.Directives;

public class Preprocessor
{
    PreprocessorContext context = new();

    private bool IsActive => context.ConditionStack.Count == 0 || context.ConditionStack.Peek();

    public string[] PreprocessFile(string[] lines, string includeDir)
    {
        lines = ProcessDirectives(lines, includeDir);
        lines = AppendIncludes(lines);
        return lines;
    }
    
    private string[] ProcessDirectives(string[] lines, string basePath)
    {
        var result = new List<string>();
        
        foreach (var line in lines)
        {
            if (!line.StartsWith("#"))
            {
                if (IsActive)
                {
                    result.Add(ReplaceDefinitions(line));
                }
                continue;
            }
            
            Directive directive = DirectiveFactory.Create(context, line);
            
            if (IsActive || directive.ignoresActive)
            {
                directive.Execute();
            }
        }
        
        return result.ToArray();
    } 

    private string GetDirectiveName(string line)
    {
        int spaceIndex = line.IndexOf(' ');
        return spaceIndex < 0 
            ? line.Substring(1) 
            : line.Substring(1, spaceIndex - 1);
    }
    
    private string[] AppendIncludes(string[] mainLines)
    {
        if (context.PendingIncludes.Count == 0)
            return mainLines;
        
        var result = new List<string>(mainLines);
        int nextPageNumber = CalculateNextPageNumber(mainLines);
        
        foreach (var include in context.PendingIncludes)
        {
            result.Add($"PAGE{nextPageNumber}:");
            result.AddRange(include);
            nextPageNumber++;
        }
        
        context.PendingIncludes.Clear();
        
        return result.ToArray();
    }
    
    private int CalculateNextPageNumber(string[] lines)
    {
        int maxPage = -1;
        
        foreach (var line in lines)
        {
            if (line.StartsWith("PAGE"))
            {
                string pageStr = line.Substring(5).TrimEnd(':');
                if (int.TryParse(pageStr, out int pageNum))
                    maxPage = Math.Max(maxPage, pageNum);
            }
        }
        
        return maxPage + 1;
    }
    
    private string ReplaceDefinitions(string line)
    {
        string result = line;
        foreach (var kvp in context.Definitions)
        {
            string pattern = @"\b" + Regex.Escape(kvp.Key) + @"\b";
            result = Regex.Replace(result, pattern, kvp.Value);
        }
        return result;
    }
    
    private bool EvaluateCondition(string condition)
    {
        foreach (var kvp in context.Definitions)
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
