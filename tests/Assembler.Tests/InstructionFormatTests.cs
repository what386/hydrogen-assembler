using Xunit;
using Assembler.Models.Formats;

namespace Assembler.Tests;

public class InstructionFormatTests
{
    [Fact]
    public void GetOperandLengths_SimpleInstruction_ReturnsCorrectLengths()
    {
        // Arrange
        var format = InstructionTable.Formats["add"];
        
        // Act
        int[] lengths = format.GetOperandLengths();
        
        // Assert
        Assert.Equal(3, lengths.Length);
        Assert.All(lengths, len => Assert.Equal(3, len));
    }
    
    [Fact]
    public void GetOperandLengths_ImmediateInstruction_ReturnsCorrectLengths()
    {
        // Arrange
        var format = InstructionTable.Formats["ldi"];
        
        // Act
        int[] lengths = format.GetOperandLengths();
        
        // Assert
        Assert.Equal(2, lengths.Length);
        Assert.Equal(3, lengths[0]); // Register
        Assert.Equal(8, lengths[1]); // Immediate
    }
}
