namespace Assembler.Models.Directives;

using Assembler.Models.Directives.ControlFlow;
using Assembler.Models.Directives.Definitions;
using Assembler.Models.Directives.Transforms;

using Assembler.Exceptions;

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
        { "end", (context, operand) => new End(context) }, // does nothing

        // definitions
        { "define", (context, operand) => new Define(context, operand) },
        { "undef", (context, operand) => new Undef(context, operand) },

        // transforms
        { "include", (context, operand) => new Include(context, operand) },
    };
    
    public static Directive Create(PreprocessorContext context, string line)
    {
        if (!line.StartsWith("#"))
            throw new AssemblerException("Attempted to create directive from unknown line");
        
        string directiveName = ExtractDirectiveName(line);
        string operand = ExtractOperands(line, directiveName);
        
        if (!directiveMap.TryGetValue(directiveName, out var factory))
            throw new DirectiveException($"Unknown directive: #{directiveName}");
        
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
