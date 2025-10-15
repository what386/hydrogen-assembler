namespace Assembler.Models.Operands;

using Assembler.Models.Formats;

public class Condition(string value, int length) : Operand(value, length, Type.CONDITION)
{
   protected override string Parse()
   {
         if(NameTable.BranchConditions.TryGetValue(value, out var result))
            return result;

         throw new KeyNotFoundException($"Condition not found in format table: '{value}'");
   } 
}
