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
            // SYSTEM
            ["nop"] = new InstructionFormat("00000", new Operand.Type[0],                       new[] { "00000000000" }),
            ["hlt"] = new InstructionFormat("00001", new Operand.Type[0],                       new[] { "00", "T01", "00000000" }),
            ["sys"] = new InstructionFormat("00010", new[] { SETTING, IMMEDIATE },              new[] { "X03", "Y08" }),
            // CONTROL FLOW
            ["cli"] = new InstructionFormat("00011", new[] { REGISTER, CONDITION, IMMEDIATE },  new[] { "X03", "Y03", "Z05" }),
            ["jmp"] = new InstructionFormat("00100", new[] { IMMEDIATE },                       new[] { "X11" }),
            ["bra"] = new InstructionFormat("00101", new[] { CONDITION, IMMEDIATE },            new[] { "X03", "T02", "Y06" }),
            ["cal"] = new InstructionFormat("00110", new[] { IMMEDIATE },                       new[] { "X11" }),
            ["ret"] = new InstructionFormat("00111", new Operand.Type[0],                       new[] { "00", "T01", "00000000" }),
            // INPUT / OUTPUT
            ["inp"] = new InstructionFormat("01000", new[] { REGISTER, ADDRESS },               new[] { "X03", "Y08" }),
            ["out"] = new InstructionFormat("01001", new[] { ADDRESS, REGISTER },               new[] { "Y03", "X08" }),
            // SPECIAL REGISTERS
            ["sld"] = new InstructionFormat("01010", new[] { REGISTER, REGISTER },              new[] { "X03", "Y03", "00000" }),
            ["sst"] = new InstructionFormat("01011", new[] { REGISTER, REGISTER },              new[] { "Y03", "X03", "00000" }),
            // MEMORY
            ["pop"] = new InstructionFormat("01100", new[] { REGISTER, IMMEDIATE},              new[] { "X03", "T02", "Y06" }),
            ["psh"] = new InstructionFormat("01101", new[] { REGISTER, IMMEDIATE},              new[] { "X03", "T02", "Y06" }),
            ["mld"] = new InstructionFormat("01110", new[] { REGISTER, ADDRESS },               new[] { "X03", "Y08" }),
            ["mst"] = new InstructionFormat("01111", new[] { ADDRESS, REGISTER },               new[] { "Y03", "X08" }),
            // REGISTERS
            ["ldi"] = new InstructionFormat("10000", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["mov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER, CONDITION},    new[] { "X03", "Y03", "T02", "Z03" }),
            // IMMEDIATE
            ["adi"] = new InstructionFormat("10010", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["ani"] = new InstructionFormat("10011", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["ori"] = new InstructionFormat("10100", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["xri"] = new InstructionFormat("10101", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["cpi"] = new InstructionFormat("10110", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            ["tsi"] = new InstructionFormat("10111", new[] { REGISTER, IMMEDIATE },             new[] { "X03", "Y08" }),
            // ARITHMETIC
            ["add"] = new InstructionFormat("11000", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["sub"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            // LOGIC
            ["bit"] = new InstructionFormat("11010", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bnt"] = new InstructionFormat("11011", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bsh"] = new InstructionFormat("11100", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["bsi"] = new InstructionFormat("11101", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            // ADVANCED MATH
            ["mdo"] = new InstructionFormat("11110", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
            ["btc"] = new InstructionFormat("11111", new[] { REGISTER, REGISTER, REGISTER },    new[] { "X03", "Y03", "T02", "Z03" }),
        }.ToImmutableDictionary();
    } 
}
