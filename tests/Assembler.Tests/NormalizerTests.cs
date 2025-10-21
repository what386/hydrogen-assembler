using Xunit;
using Assembler.Core;

namespace Assembler.Tests;

public class NormalizerTests 
{
    [Fact]
    public void Normalizer_AlignPages_CorrectsPageSize()
    {
        Normalizer normalizer = new();

        string[] lines = 
        {
            "PAGE0:",
            "add r1, r2, r3",
            "add r3, r2, r1",
            "PAGE1:",
            "sub r1, r2, r3",
        };

        string[] expected =
        {
            "add r1, r2, r3",
            "add r3, r2, r1",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "sub r1, r2, r3"
        };

        var actual = normalizer.NormalizeLines(lines);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Normalizer_ProcessLabels_RetursCorrectAddresses()
    {
        Normalizer normalizer = new();

        string[] lines = 
        {
            "PAGE0:",
            "jmp .label3",
            "nop",
            "nop",
            "nop",
            "label2:",
            "jmp .PAGE0",
            "jmp .label2",
            "nop",
            "label3:",
            "nop",
            "jmp .label2"
        };

        string[] expected =
        {
            "jmp !7",
            "nop",
            "nop",
            "nop",
            "jmp !0",
            "jmp !4",
            "nop",
            "nop",
            "jmp !4",
        };

        var actual = normalizer.NormalizeLines(lines);
        
        Assert.Equal(expected, actual);
    }
}
