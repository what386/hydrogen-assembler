namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Setting(string name) : Operand(name, LENGTH, Type.SETTING)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.Settings.TryGetValue(base.value, out var result))
            return result;

         throw new KeyNotFoundException($"Setting not found in format table: '{base.value}'");
   } 
}
