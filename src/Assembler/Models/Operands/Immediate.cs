namespace Assembler.Models.Operands;

using Assembler.Utils;
using Assembler.Exceptions;

public class Immediate(string name, int? length = null) : Operand(name, length ?? 0, Type.IMMEDIATE)
{
   public override string Parse()
   {
      int number = BaseConverter.ToInteger(value);
         
      if(number > ((1 << base.length) - 1))
         throw new SemanticException("Operand length mismatch");

      return BaseConverter.ToBinary(number, base.length);
   }
}
