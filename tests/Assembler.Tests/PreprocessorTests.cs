using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class PreprocessorTests 
{
    [Fact]
    public void Preprocessor_ElseDirective_InversionCorrect()
    {
        Preprocessor preprocessor = new();

        string[] lines = 
        {
            "#define x 1",
            "#define y 2",
            "#define z 3",

            "#if x + y = z",
            "#define cond1 true1",
            "#else",
            "#define cond1 false2",
            "#endif",
            "cond1",

            "#if (z > 2) AND (x < 10)",
            "#define cond2 true2",
            "#else",
            "#define cond2 false2",
            "#endif",
            "cond2",

            "#if z<x",
            "#define cond3 true3",
            "#else",
            "#define cond3 false3",
            "#endif",
            "cond3",
        };

        string[] expected =
        {
            "true1",
            "true2",
            "false3",
        };

        var actual = preprocessor.PreprocessLines(lines, "none");
        
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Preprocessor_IfDirective_ComparisonCorrect()
    {
        Preprocessor preprocessor = new();

        string[] lines = 
        {
            "#define x 1",
            "#define y 2",
            "#define z 3",

            "#if x + y = z",
            "#define cond1 pass1",
            "#endif",
            "cond1",

            "#if (z > 2) AND (x < 10)",
            "#define cond2 pass2",
            "#endif",
            "cond2",

            "#if z<x",
            "#define cond3 fail3",
            "#endif",
            "cond3",
        };

        string[] expected =
        {
            "pass1",
            "pass2",
            "cond3",
        };

        var actual = preprocessor.PreprocessLines(lines, "none");
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Preprocessor_Ifdef_ChecksValue()
    {
        Preprocessor preprocessor = new();

        string[] lines = 
        {
            "#define value 39",

            "#ifdef value",
            "#define cond1 pass1",
            "#endif",

            "#ifndef valueother",
            "#define cond2 pass2",
            "#endif",

            "cond1",
            "cond2",
        };

        string[] expected =
        {
            "pass1",
            "pass2",
        };

        var actual = preprocessor.PreprocessLines(lines, "none");
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Preprocessor_RemoveComments_ProducesCleanAssembly()
    {
        Preprocessor preprocessor = new();

        string[] lines = 
        {
            "add r1, r2, r3",
            "; comment",
            "sub r3, r2, r1",
            "; comment 1",
            "; comment 2",
            "adc r2 r1 r3 ; inline comment",
            "sbb r3 r1 r2 ; inline comment with another ':'",
            "bsh r7 r7 r3",
        };

        string[] expected =
        {
            "add r1, r2, r3",
            "sub r3, r2, r1",
            "adc r2 r1 r3",
            "sbb r3 r1 r2",
            "bsh r7 r7 r3",
        };

        var actual = preprocessor.PreprocessLines(lines, "none");
        
        Assert.Equal(expected, actual);
    }
}
