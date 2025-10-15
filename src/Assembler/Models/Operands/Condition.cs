namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Condition(string value) : Operand(value, 3, Type.CONDITION)
{
   public override string Parse()
   {
         if(NameTable.BranchConditions.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Condition not found in format table: '{value}'");
   } 
}
