namespace Assembler.Models.Operands;

using Assembler.Models.Formats;
using Assembler.Exceptions;

public class Setting(string name) : Operand(name, LENGTH, Type.SETTING)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.Settings.TryGetValue(base.value, out var result))
            return result;

         throw new SemanticException($"Setting not found in format table: '{base.value}'");
   } 
}
