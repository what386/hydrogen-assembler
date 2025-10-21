namespace Assembler;

using Assembler.Core;
using Assembler.Models;
using Assembler.Exceptions;

public class Program
{
    static Preprocessor preprocessor = new();
    static Normalizer normalizer = new();
    static Lexer lexer = new();
    static Parser parser = new();

    static int Main(string[] args)
    {
        if (args.Length is < 1 or > 2)
        {
            Console.Error.WriteLine("Usage: assembler <input.asm> [output.bin]");
            return 1;
        }
        
        string inputFile = args[0];
        string? outputFile = args.Length > 1 ? args[1] : null;

        string[] lines = File.ReadAllLines(inputFile);
        string[] binary;

        try
        {
            lines = preprocessor.PreprocessLines(lines, inputFile);
            lines = normalizer.NormalizeLines(lines);
            Instruction[] instructions = lexer.GetInstructions(lines);
            binary = parser.ParseInstructions(instructions);
        }
        catch(DirectiveException ex)
        {
            Console.WriteLine($"Directive error: {ex.Message}");
            Console.WriteLine("Stop.");
            return 1;
        }
        catch(FormatException ex)
        {
            Console.WriteLine($"Format error: {ex.Message}");
            Console.WriteLine("Stop.");
            return 1;
        }
        catch(SyntaxException ex)
        {
            Console.WriteLine($"Syntax error: {ex.Message}");
            Console.WriteLine("Stop.");
            return 1;
        }
        catch(SemanticException ex)
        {
            Console.WriteLine($"Semantic error: {ex.Message}");
            Console.WriteLine("Stop.");
            return 1;
        }

        if (outputFile != null)
        {
            File.WriteAllLines(outputFile, binary);
            Console.WriteLine($"Output written to {outputFile}");
        }
        else
        {
            foreach (var line in binary)
                Console.WriteLine(line);
        }

        return 0;
    }
}
