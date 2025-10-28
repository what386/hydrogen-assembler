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
        string[] binaryLines;

        try
        {
            lines = preprocessor.PreprocessLines(lines, inputFile);
            lines = normalizer.NormalizeLines(lines);
            Instruction[] instructions = lexer.GetInstructions(lines);
            binaryLines = parser.ParseInstructions(instructions);
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
            byte[] bytes = binaryLines
                .SelectMany(binary => 
                {
                    ushort value = Convert.ToUInt16(binary, 2); // Convert binary string to ushort
                    return new byte[] 
                    { 
                        (byte)(value >> 8),   // High byte
                        (byte)(value & 0xFF)  // Low byte
                    };
                })
                .ToArray();

            File.WriteAllBytes(outputFile, bytes);
            Console.WriteLine($"Output written to {outputFile}");
        }
        else
        {
            foreach (var line in binaryLines)
                Console.WriteLine(line);
        }

        return 0;
    }
}
