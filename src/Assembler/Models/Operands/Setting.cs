namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Setting(string value) : Operand(value, LENGTH, Type.SETTING)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.Settings.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Setting not found in format table: '{value}'");
   } 
}
