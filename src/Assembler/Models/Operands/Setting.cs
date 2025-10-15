namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Setting(string value) : Operand(value, 3, Type.SETTING)
{
   public override string Parse()
   {
         if(NameTable.Settings.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Setting not found in format table: '{value}'");
   } 
}
