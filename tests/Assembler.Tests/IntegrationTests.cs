using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class IntegrationTests
{
    [Theory]
    [InlineData("nop",              "0000000000000000")]
    [InlineData("exit",             "0000100100000000")]
    [InlineData("adi r7, !4",       "1001011100000100")]
    [InlineData("nand r7, r1, r3",  "1101111100101011")]
    [InlineData("brt ?eq, !20",     "0010100010010100")]
    [InlineData("jmp !2047",        "0010011111111111")] // 11-bit immediate
    public void FullAssembly_ValidInstruction_ProducesCorrectBinary(string source, string expected)
    {
        Lexer lexer = new();
        Parser parser = new();

        var instruction = lexer.GetInstruction(source);
        string binary = parser.ParseInstruction(instruction);
        
        Assert.Equal(expected, binary);
    }
}
