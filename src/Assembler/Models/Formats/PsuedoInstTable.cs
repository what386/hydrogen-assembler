namespace Assembler.Models.Formats;

using System.Collections.Immutable;
using static Assembler.Models.Operands.Operand.Type;
using Assembler.Models.Operands;

public static class PseudoInstTable
{
    public static ImmutableDictionary<string, InstructionFormat> Formats { get; }

    static PseudoInstTable()
    {
        Formats = new Dictionary<string, InstructionFormat>
        {
            //OP                            OPCODE           X,        Y,        Z          MASK
            ["nop"] = new InstructionFormat("00000", new Operand.Type[0],           new[] { "000", "00000000" }),
            ["hlt"] = new InstructionFormat("00000", new Operand.Type[0],           new[] { "001", "00000000" }),
            ["ret"] = new InstructionFormat("00111", new[] { REGISTER },            new[] { "X03", "T02", "000000" }),
            ["mli"] = new InstructionFormat("01110", new[] { REGISTER, REGISTER },  new[] { "X03", "Y03", "00000" }),
            ["msi"] = new InstructionFormat("01111", new[] { REGISTER, REGISTER },  new[] { "Y03", "X03", "00000" }),
            ["mov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER },  new[] { "X03", "Y03", "T02", "111" }),
            ["neg"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER },  new[] { "X03", "000", "00", "Y03" }),
            ["cmp"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER },  new[] { "000", "X03", "00", "Y03" }),
            ["inc"] = new InstructionFormat("10010", new[] { REGISTER },            new[] { "X03", "0000001" }),
            ["dec"] = new InstructionFormat("10010", new[] { REGISTER },            new[] { "X03", "1111111" }),
            ["not"] = new InstructionFormat("11011", new[] { REGISTER, REGISTER },  new[] { "X03", "Y03", "00", "000" }),
        }.ToImmutableDictionary();
    } 
}
