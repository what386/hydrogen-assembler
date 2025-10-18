using Xunit;
using Assembler.Models.Operands;
using Assembler.Exceptions;

namespace Assembler.Tests;

public class OperandTests
{
    [Theory]
    [InlineData("!0", 3, "000")]
    [InlineData("!7", 3, "111")]
    [InlineData("!255", 8, "11111111")]
    public void Immediate_Parse_ReturnsCorrectBinary(string value, int length, string expected)
    {
        Immediate immediate = new(value, length);
        string result = immediate.Parse();
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("r0", "000")]
    [InlineData("r5", "101")]
    [InlineData("r7", "111")]
    public void Register_Parse_ReturnsCorrectBinary(string value, string expected)
    {
        var register = new Register(value);
        string result = register.Parse();
        
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData("r-1")]
    [InlineData("r8")]
    [InlineData("r99")]
    public void Register_IndexMismatch_ThrowsSemanticException(string reg)
    {
        var register = new Register(reg);
        
        Assert.Throws<SemanticException>(() => (register.Parse()));
    }

    [Fact]
    public void Setting_UnknownSetting_ThrowsSemanticException()
    {
        var setting = new Setting("[err]");
        
        Assert.Throws<SemanticException>(() => (setting.Parse()));
    }

    [Fact]
    public void Condition_UnknownCondition_ThrowsSemanticException()
    {
        var condition = new Condition("?err");
        
        Assert.Throws<SemanticException>(() => condition.Parse());
    }
    
    [Fact]
    public void Immediate_ValueTooLarge_ThrowsException()
    {
        var immediate = new Immediate("256", 8); // 256 doesn't fit in 8 bits
        
        Assert.Throws<SemanticException>(() => immediate.Parse());
    }
}
