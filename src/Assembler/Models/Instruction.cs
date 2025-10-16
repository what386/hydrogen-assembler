namespace Assembler.Models;

using Assembler.Models.Operands;

public record Instruction(string mnemonic, Operand[] operands)
{
    public string mnemonic = mnemonic;
    public Operand[] operands = operands;
}
