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
            // The operand mask maps where operands are in the final instruction.
            // X, Y and Z are the first, second and third operands respectively.
            // T is for the instruction type, which is determined by the mnemonic.

            // MNEMONIC,                    OPCODE          OPERAND FORMATS                     OPERAND MASK  

            // SYSTEM
            ["nop"] = new InstructionFormat("00000", new[] { NONE },                            "00000000000"),
            ["hlt"] = new InstructionFormat("00001", new[] { NONE },                            "00T00000000"),
            ["sys"] = new InstructionFormat("00010", new[] { SETTING, IMMEDIATE },              "AAABBBBBBBB"),

            // CONTROL FLOW
            ["cli"] = new InstructionFormat("00011", new[] { REGISTER, CONDITION, IMMEDIATE },  "XXXYYYZZZZZ"),
            ["jmp"] = new InstructionFormat("00100", new[] { IMMEDIATE },                       "XXXXXXXXXXX"),
            ["bra"] = new InstructionFormat("00101", new[] { CONDITION, IMMEDIATE },            "XXXTTYYYYYY"),
            ["cal"] = new InstructionFormat("00110", new[] { IMMEDIATE },                       "XXXXXXXXXXX"),
            ["ret"] = new InstructionFormat("00111", new[] { NONE },                            "00T00000000"),

            // INPUT / OUTPUT
            ["inp"] = new InstructionFormat("01000", new[] { REGISTER, ADDRESS },               "XXXYYYYYYYY"),
            ["out"] = new InstructionFormat("01001", new[] { ADDRESS, REGISTER },               "YYYXXXXXXXX"),

            // SPECIAL REGISTERS
            ["sld"] = new InstructionFormat("01010", new[] { REGISTER, REGISTER },              "XXXYYY00000"),
            ["sst"] = new InstructionFormat("01011", new[] { REGISTER, REGISTER },              "YYYXXX00000"),

            // MEMORY
            ["pop"] = new InstructionFormat("01100", new[] { REGISTER, IMMEDIATE},              "XXXTTYYYYYY"),
            ["psh"] = new InstructionFormat("01101", new[] { REGISTER, IMMEDIATE},              "XXXTTYYYYYY"),
            ["mld"] = new InstructionFormat("01110", new[] { REGISTER, ADDRESS },               "XXXYYYYYYYY"),
            ["mst"] = new InstructionFormat("01111", new[] { ADDRESS, REGISTER },               "YYYXXXXXXXX"),

            // REGISTERS
            ["ldi"] = new InstructionFormat("10000", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["mov"] = new InstructionFormat("10001", new[] { REGISTER, REGISTER, CONDITION},    "XXXYYYTTZZZ"),

            // IMMEDIATE
            ["adi"] = new InstructionFormat("10010", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["ani"] = new InstructionFormat("10011", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["ori"] = new InstructionFormat("10100", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["xri"] = new InstructionFormat("10101", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["cpi"] = new InstructionFormat("10110", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),
            ["tsi"] = new InstructionFormat("10111", new[] { REGISTER, IMMEDIATE },             "XXXYYYYYYYY"),

            // ARITHMETIC
            ["add"] = new InstructionFormat("11000", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
            ["sub"] = new InstructionFormat("11001", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),

            // LOGIC
            ["bit"] = new InstructionFormat("11010", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
            ["bnt"] = new InstructionFormat("11011", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
            ["bsh"] = new InstructionFormat("11100", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
            ["bsi"] = new InstructionFormat("11101", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),

            // ADVANCED MATH
            ["mul"] = new InstructionFormat("11110", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
            ["btc"] = new InstructionFormat("11111", new[] { REGISTER, REGISTER, REGISTER },    "XXXYYYTTZZZ"),
        }.ToImmutableDictionary();
    }

    public record InstructionFormat(string opcode, OperandType[] operands, string pattern)
    {
        string opcode = opcode;
        OperandType[] operands = operands;
        string? pattern = pattern;
    }
}


