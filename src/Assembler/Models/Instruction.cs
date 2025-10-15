namespace Assembler.Models;

using Assembler.Models.Operands;

public record Instruction(string mnemonic, Operand[] operands, string? bits)
{
    string mnemonic = mnemonic;
    Operand[]? operands = operands;
    string? typeBits = bits;
}
