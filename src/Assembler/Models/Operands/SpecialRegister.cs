namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class SpecialRegister(string value, int length) : Operand(value, length, Type.SPECIALREG)
{
   protected override string Parse()
   {
         if(NameTable.SpecialRegisters.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Special register not found in format table: '{value}'");
   }
}
