using Xunit;
using Assembler.Models.Operands;

namespace Assembler.Tests;

public class OperandTests
{
    [Theory]
    [InlineData("!0", 3, "000")]
    [InlineData("!7", 3, "111")]
    [InlineData("!255", 8, "11111111")]
    public void Immediate_Parse_ReturnsCorrectBinary(string value, int length, string expected)
    {
        // Arrange
        var immediate = new Immediate(value, length);
        
        // Act
        string result = immediate.Parse();
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("0", "000")]
    [InlineData("5", "101")]
    [InlineData("7", "111")]

    public void Register_Parse_ReturnsCorrectBinary(string value, string expected)
    {
        // Arrange
        var register = new Register(value);
        
        // Act
        string result = register.Parse();
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Immediate_ValueTooLarge_ThrowsException()
    {
        // Arrange
        var immediate = new Immediate("256"); // 256 doesn't fit in 8 bits
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => immediate.Parse());
    }
}
