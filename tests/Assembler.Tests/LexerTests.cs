using Xunit;
using Assembler.Core;
using Assembler.Models;
using Assembler.Models.Operands;
using Assembler.Exceptions;

namespace Assembler.Tests;

public class LexerTests
{
    [Fact]
    public void GetInstructions_Assembly_ReturnsCorrectInstructions()
    {
        Lexer lexer = new();

        string[] lines = 
        {
            "nop !0",
            "brt ?eq, \"E\"",
            "call !1067",
            "pop r7",
            "poke r1, !0",
            // this instruction doesne exist, but the lexer should not care
            "xri r3, +2",
            "sys $ctrl, !83",
            "add r1, r2, r3",
        };

        Instruction[] expected =
        {
            new Instruction("nop", new Operand[] { new Immediate("!0") }),
            new Instruction("brt", new Operand[] { new Condition("?eq"), new Character("\"E\"") }),
            new Instruction("call", new Operand[] { new Immediate("!1067") }),
            new Instruction("pop", new Operand[] { new Register("r7") }),
            new Instruction("poke", new Operand[] { new Register("r1"), new Immediate("!0") }),
            new Instruction("xri", new Operand[] { new Register("r3"), new Offset("+2") }),
            new Instruction("sys", new Operand[] { new Setting("$ctrl"), new Immediate("!83") }),
            new Instruction("add", new Operand[] { new Register("r1"), new Register("r2"), new Register("r3") }),
        }; 

        var actual = lexer.GetInstructions(lines);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseLine_UnknownOperand_ThrowsSyntaxException()
    {
        Lexer lexer = new();

        var line = "add *20, r2, r3";
        
        Assert.Throws<SyntaxException>(() => lexer.GetInstruction(line));
    }

    [Fact]
    public void ParseLine_MissingPrefix_ThrowsSyntaxException()
    {
        Lexer lexer = new();
        
        var line = "lim 1, 86";

        Assert.Throws<SyntaxException>(() => lexer.GetInstruction(line));
    }
    
    [Fact]
    public void ParseLine_MissingCommas_ThrowsSyntaxException()
    {
        Lexer lexer = new();

        var line = "add r1 r2 r3";
        
        Assert.Throws<SyntaxException>(() => lexer.GetInstruction(line));
    }
}
