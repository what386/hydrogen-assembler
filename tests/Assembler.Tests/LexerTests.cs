using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class LexerTests
{
    [Fact]
    public void ParseLine_SimpleInstruction_ReturnsCorrectMnemonic()
    {
        Lexer lexer = new();

        string line = "add r1, r2, r3";
        var instruction = lexer.GetInstruction(line);
        
        Assert.Equal("add", instruction.mnemonic);
        Assert.Equal(3, instruction.operands.Length);
    }
    
    [Fact]
    public void ParseLine_NoOperands_ReturnsEmptyOperandArray()
    {
        Lexer lexer = new Lexer();

        string line = "nop";
        var instruction = lexer.GetInstruction(line);
        
        Assert.Equal("nop", instruction.mnemonic);
        Assert.Empty(instruction.operands);
    }
}
