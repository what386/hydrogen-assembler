namespace Assembler.Models.Formats;

using System.Collections.Immutable;
using static Assembler.Models.Operands.Operand.Type;
using OperandType = Assembler.Models.Operands.Operand.Type;

public static class InstructionTable
{
    public static ImmutableDictionary<string, InstructionFormat> Formats { get; }
    
   static InstructionTable()
{
    Formats = new Dictionary<string, InstructionFormat>
    {
        // SYSTEM
        ["nop"] = new InstructionFormat("00000", new[] { NONE },                            new[] { "00000000000" }),
        ["hlt"] = new InstructionFormat("00001", new[] { NONE },                            new[] { "00", "T", "00000000" }),
        ["sys"] = new InstructionFormat("00010", new[] { SETTING, IMMEDIATE },              new[] { "XXX", "YYYYYYYY" }),
        // CONTROL FLOW
        ["cli"] = new InstructionFormat("00011", new[] { REGISTER, CONDITION, IMMEDIATE },  new[] { "XXX", "YYY", "ZZZZZ" }),
        ["jmp"] = new InstructionFormat("00100", new[] { IMMEDIATE },                       new[] { "XXXXXXXXXXX" }),
        ["bra"] = new InstructionFormat("00101", new[] { CONDITION, IMMEDIATE },            new[] { "XXX", "TT", "YYYYYY" }),
        ["cal"] = new InstructionFormat("00110", new[] { IMMEDIATE },                       new[] { "XXXXXXXXXXX" }),
        ["ret"] = new InstructionFormat("00111", new[] { NONE },                            new[] { "00", "T", "00000000" }),
        // INPUT / OUTPUT
        ["inp"] = new InstructionFormat("01000", new[] { REGISTER, ADDRESS },               new[] { "XXX", "YYYYYYYY" }),
        ["out"] = new InstructionFormat("01001", new[] { ADDRESS, REGISTER },               new[] { "YYY", "XXXXXXXX" }),
        // SPECIAL REGISTERS
        ["sld"] = new InstructionFormat("01010", new[] { REGISTER, REGISTER },              new[] { "XXX", "YYY", "00000" }),
        ["sst"] = new InstructionFormat("01011", new[] { REGISTER, REGISTER },              new[] { "YYY", "XXX", "00000" }),
        // MEMORY
        ["pop"] = new InstructionFormat("01100", new[] { REGISTER, IMMEDIATE},              new[] { "XXX", "TT", "YYYYYY" }),
        ["psh"] = new InstructionFormat("01101", new[] { REGISTER, IMMEDIATE},              new[] { "XXX", "TT", "YYYYYY" }),
        ["mld"] = new InstructionFormat("01110", new[] { REGISTER, ADDRESS },               new[] { "XXX", "YYYYYYYY" }),
        ["mst"] = new InstructionFormat("01111", new[] { ADDRESS, REGISTER },               new[] { "YYY", "XXXXXXXX" }),
        // REGISTERS
        ["ldi"] = new InstructionFormat("10000", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["mov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER, CONDITION},    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        // IMMEDIATE
        ["adi"] = new InstructionFormat("10010", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["ani"] = new InstructionFormat("10011", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["ori"] = new InstructionFormat("10100", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["xri"] = new InstructionFormat("10101", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["cpi"] = new InstructionFormat("10110", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        ["tsi"] = new InstructionFormat("10111", new[] { REGISTER, IMMEDIATE },             new[] { "XXX", "YYYYYYYY" }),
        // ARITHMETIC
        ["add"] = new InstructionFormat("11000", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        ["sub"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        // LOGIC
        ["bit"] = new InstructionFormat("11010", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        ["bnt"] = new InstructionFormat("11011", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        ["bsh"] = new InstructionFormat("11100", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        ["bsi"] = new InstructionFormat("11101", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        // ADVANCED MATH
        ["mul"] = new InstructionFormat("11110", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
        ["btc"] = new InstructionFormat("11111", new[] { REGISTER, REGISTER, REGISTER },    new[] { "XXX", "YYY", "TT", "ZZZ" }),
    }.ToImmutableDictionary();
} 

    public record InstructionFormat(string opcode, OperandType[] operands, string[] maskSegments)
    {
        string opcode = opcode;
        OperandType[] operands = operands;
        string[]? maskSegments = maskSegments;
    }
}
