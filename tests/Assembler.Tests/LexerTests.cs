using Xunit;
using Assembler.Core;
using Assembler.Models.Operands;

namespace Assembler.Tests;

public class LexerTests
{
    [Fact]
    public void ParseLine_SimpleInstruction_ReturnsCorrectMnemonic()
    {
        // Arrange
        string line = "add r1, r2, r3";
        
        // Act
        var instruction = Lexer.GetInstruction(line);
        
        // Assert
        Assert.Equal("add", instruction.mnemonic);
        Assert.Equal(3, instruction.operands.Length);
    }
    
    [Fact]
    public void ParseLine_NoOperands_ReturnsEmptyOperandArray()
    {
        // Arrange
        string line = "nop";
        
        // Act
        var instruction = Lexer.GetInstruction(line);
        
        // Assert
        Assert.Equal("nop", instruction.mnemonic);
        Assert.Empty(instruction.operands);
    }
}
