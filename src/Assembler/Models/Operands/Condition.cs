namespace Assembler.Models.Operands;

using Assembler.Models.Formats;
using Assembler.Exceptions;

public class Condition(string name) : Operand(name, 3, Type.CONDITION)
{
   const int LENGTH = 3;

   public override string Parse()
   {
         if(NameTable.BranchConditions.TryGetValue(base.value, out var result))
            return result;

         throw new SemanticException($"Condition not found in format table: '{base.value}'");
   } 
}
