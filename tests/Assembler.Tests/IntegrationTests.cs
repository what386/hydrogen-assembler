using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class IntegrationTests
{
    [Fact]
    public void Assembler_RawAssembly_ReturnsCorrectBinary()
    {
        Lexer lexer = new();
        Parser parser = new();

        string[] lines = 
        {
            // TODO: add optional parameters
            "nop",
            "hlt",
            "addi r7, !4",
            "nand r7, r1, r3",
            "brt ?eq, !20",
            "jmp !2047"
        };

        string[] expected =
        {
            "0000000000000000",
            "0000011000000000",
            "1001011100000100",
            "1101111100101011",
            "0010100001010100",
            "0010011111111111"
        }; 

        var instructions = lexer.GetInstructions(lines);
        string[] actual = parser.ParseInstructions(instructions);
        
        Assert.Equal(expected, actual);
    }
}
