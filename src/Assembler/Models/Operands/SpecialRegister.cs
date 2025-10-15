namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class SpecialRegister(string value) : Operand(value, LENGTH, Type.SPECIALREG)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.SpecialRegisters.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Special register not found in format table: '{value}'");
   }
}
