namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class SpecialRegister(string value) : Operand(value, 3, Type.SPECIALREG)
{
   public override string Parse()
   {
         if(NameTable.SpecialRegisters.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Special register not found in format table: '{value}'");
   }
}
