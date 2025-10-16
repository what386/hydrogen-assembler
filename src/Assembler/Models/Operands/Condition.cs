namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Condition(string name) : Operand(name, 3, Type.CONDITION)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.BranchConditions.TryGetValue(base.value, out var result))
            return result;

         throw new KeyNotFoundException($"Condition not found in format table: '{base.value}'");
   } 
}
