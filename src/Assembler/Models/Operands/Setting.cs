namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Setting(string value, int length) : Operand(value, length, Type.SETTING)
{
   protected override string Parse()
   {
         if(NameTable.Settings.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException();
   } 
}
