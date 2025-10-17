using Xunit;
using Assembler.Core;
using Assembler.Models;
using Assembler.Models.Operands;

namespace Assembler.Tests;

public class ParserTests
{
    [Fact]
    public void ParseInstruction_SimpleAdd_ReturnsCorrectBinary()
    {
        Parser parser = new();

        var instruction = new Instruction(
            "add",
            new Operand[]
            {
                new Register("1"),
                new Register("2"),
                new Register("3")
            }
        );
        
        string binary = parser.ParseInstruction(instruction);
        
        Assert.Equal(16, binary.Length); // 5-bit opcode + 11-bit operands
        Assert.StartsWith("11000", binary); // ADD opcode
    }
    
    [Fact]
    public void ParseInstruction_UnknownMnemonic_ThrowsArgumentException()
    {
        Parser parser = new();

        var instruction = new Instruction("invalid", Array.Empty<Operand>());
        
        Assert.Throws<ArgumentException>(() => parser.ParseInstruction(instruction));
    }
    
    [Fact]
    public void ParseInstruction_OperandLengthMismatch_ThrowsArgumentException()
    {
        Parser parser = new();


        var instruction = new Instruction(
            "ldi",
            new Operand[]
            {
                new Register("1"),
                new Immediate("1024", 8) // max is 255
            }
        );

        Assert.Throws<ArgumentException>(() => parser.ParseInstruction(instruction));
    }
}
