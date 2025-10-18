namespace Assembler.Models.Directives;

using Assembler.Models.Directives.ControlFlow;
using Assembler.Models.Directives.Definitions;
using Assembler.Models.Directives.Transforms;

public class DirectiveFactory
{
    private static readonly Dictionary<string, Func<PreprocessorContext, string, Directive>> directiveMap = new()
    {
        // control flow
        { "if", (context, operand) => new If(context, operand) },
        { "ifdef", (context, operand) => new Ifdef(context, operand) },
        { "ifndef", (context, operand) => new Ifndef(context, operand) },
        { "else", (context, operand) => new Else(context) },
        { "endif", (context, operand) => new Endif(context) },

        // definitions
        { "define", (context, operand) => new Define(context, operand) },

        // transforms
        { "include", (context, operand) => new Include(context, operand) },
    };
    
    public static Directive Create(PreprocessorContext context, string line)
    {
        if (!line.StartsWith("#"))
            throw new ArgumentException("Not a directive");
        
        string directiveName = ExtractDirectiveName(line);
        string operand = ExtractOperands(line, directiveName);
        
        if (!directiveMap.TryGetValue(directiveName, out var factory))
            throw new ArgumentException($"Unknown directive: #{directiveName}");
        
        return factory(context, operand);
    }
    
    private static string ExtractDirectiveName(string line)
    {
        // indexof returns -1 if not found
        int spaceIndex = line.IndexOf(' ');

        return spaceIndex < 0 
            ? line.Substring(1) 
            : line.Substring(1, spaceIndex - 1);
    }
    
    private static string ExtractOperands(string line, string directiveName)
    {
        int startIndex = directiveName.Length + 1; // +1 for '#'
        return startIndex < line.Length 
            ? line.Substring(startIndex).Trim() 
            : string.Empty;
    }
}
