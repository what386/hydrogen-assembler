namespace Assembler.Models;

using Assembler.Models.Operands;

public record Instruction(string mnemonic, Operand[] operands)
{
    public string mnemonic = mnemonic;
    public Operand[] operands = operands;

   public override string ToString()
    {
        if (operands == null || operands.Length == 0)
            return mnemonic;
        
        return $"{mnemonic} {string.Join(", ", operands.AsEnumerable())}";
    } 
}
