namespace Assembler.Models;

using Assembler.Models.Operands;

public record Instruction(string mnemonic, Operand[] operands)
{
    public virtual bool Equals(Instruction? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return string.Equals(mnemonic, other.mnemonic, StringComparison.OrdinalIgnoreCase) &&
               operands.SequenceEqual(other.operands);
    }
    
    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(mnemonic, StringComparer.OrdinalIgnoreCase);
        foreach (var operand in operands)
            hash.Add(operand);
        return hash.ToHashCode();
    } 

    public override string ToString()
    {
        if (operands == null || operands.Length == 0)
            return mnemonic;
        
        return $"{mnemonic} {string.Join(", ", operands.AsEnumerable())}";
    } 
}
