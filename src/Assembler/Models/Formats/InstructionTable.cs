namespace Assembler.Models.Formats;

using System.Collections.Immutable;

using Assembler.Models.Operands;
using static Assembler.Models.Operands.Operand.Type;

public static class InstructionTable
{
    public static ImmutableDictionary<string, InstructionFormat> Formats { get; }
    
    static InstructionTable()
    {
        Formats = new Dictionary<string, InstructionFormat>
        {
            //OP                            OPCODE           X,        Y,        Z                       MASK
            ["hlt"] = new InstructionFormat("00000", new Operand.Type[0],                       new[] { "T02", "000000000" }), // SYSTEM
            ["sys"] = new InstructionFormat("00001", new[] { SETTING, REGISTER, REGISTER},      new[] { "X03", "Y03", "T02", "Z03" }),
            ["inp"] = new InstructionFormat("00010", new[] { REGISTER, ADDRESS },               new[] { "X03", "Y08" }), // IO
            ["out"] = new InstructionFormat("00011", new[] { ADDRESS, REGISTER },               new[] { "Y03", "X08" }),
            ["jmp"] = new InstructionFormat("00100", new[] { IMMEDIATE },                       new[] { "X11" }), // CTRL FLOW
            ["bra"] = new InstructionFormat("00101", new[] { CONDITION, IMMEDIATE },            new[] { "X03", "T02", "Y06" }),
            ["cal"] = new InstructionFormat("00110", new[] { IMMEDIATE },                       new[] { "X11" }),
            ["ret"] = new InstructionFormat("00111", new Operand.Type[0],                       new[] { "T02", "000000000" }),
            ["inst"] = new InstructionFormat("01000", new[] { REGISTER, REGISTER, REGISTER },   new[] { "X03", "Y03", "T02", "Z03" }), // MEMORY
            ["blit"] = new InstructionFormat("01001", new[] { REGISTER, REGISTER, REGISTER },   new[] { "X03", "Y03", "T02", "Z03" }),
            ["pop"] = new InstructionFormat("01010", new[] { REGISTER, OFFSET },                new[] { "X03", "T02", "Y06" }),
            ["psh"] = new InstructionFormat("01011", new[] { REGISTER, OFFSET },                new[] { "X03", "T02", "Y06" }),
            ["mld"] = new InstructionFormat("01100", new[] { REGISTER, ADDRESS },               new[] { "X03", "Y08" }),
            ["mst"] = new InstructionFormat("01101", new[] { ADDRESS, REGISTER },               new[] { "Y03", "X08" }),
            ["mli"] = new InstructionFormat("01110", new[] { REGISTER, REGISTER },              new[] { "X03", "Y03", "00000" }),
            ["msi"] = new InstructionFormat("01111", new[] { REGISTER, REGISTER },              new[] { "Y03", "X03", "00000" }),
            ["mlx"] = new InstructionFormat("01110", new[] { REGISTER, REGISTER, OFFSET },      new[] { "X03", "Y03", "Z05" }),
            ["msx"] = new InstructionFormat("01111", new[] { REGISTER, REGISTER, OFFSET },      new[] { "Y03", "X03", "Z05" }),
            ["ldi"] = new InstructionFormat("10000", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }), // REGISTERS
            ["mov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER, CONDITION},    new[] { "X03", "Y03", "T02", "000" }),
            ["cmov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER, CONDITION},   new[] { "X03", "Y03", "T02", "Z03" }),
            ["addi"] = new InstructionFormat("10010", new[] { REGISTER, IMMEDIATE },            new[] { "X03", "Y08" }), // IMMEDIATE
            ["andi"] = new InstructionFormat("10011", new[] { REGISTER, IMMEDIATE },            new[] { "X03", "Y08" }),
            ["ori"] = new InstructionFormat("10100", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["xori"] = new InstructionFormat("10101", new[] { REGISTER, IMMEDIATE },            new[] { "X03", "Y08" }),
            ["cmpi"] = new InstructionFormat("10110", new[] { REGISTER, IMMEDIATE },            new[] { "X03", "Y08" }),
            ["tsti"] = new InstructionFormat("10111", new[] { REGISTER, IMMEDIATE },            new[] { "X03", "Y08" }),
            ["add"] = new InstructionFormat("11000", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }), // ARITHMETIC
            ["sub"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bit"] = new InstructionFormat("11010", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }), // LOGIC
            ["bnt"] = new InstructionFormat("11011", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bsh"] = new InstructionFormat("11100", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bshi"] = new InstructionFormat("11101", new[] { REGISTER, REGISTER, REGISTER },   new[] { "X03", "Y03", "T02", "Z03" }),
            ["mdo"] = new InstructionFormat("11110", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }), // COMPLEX MATH
            ["btc"] = new InstructionFormat("11111", new[] { REGISTER, REGISTER, IMMEDIATE },   new[] { "X03", "Y03", "T02", "Z03" }),
        }.ToImmutableDictionary();
    } 
}
