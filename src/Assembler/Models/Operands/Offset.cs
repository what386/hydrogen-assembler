namespace Assembler.Models.Operands;
using Assembler.Utils;
using Assembler.Exceptions;

public class Offset(string name, int? length = null) : Operand(name, length ?? 0, Type.OFFSET)
{
   public override string Parse()
   {
      int number = BaseConverter.ToInteger(value);
      
      int minSigned = -(1 << (base.length - 1));
      int maxSigned = (1 << (base.length - 1)) - 1;
      
      if (number < minSigned || number > maxSigned)
         throw new SemanticException("Operand length mismatch");
      
      return BaseConverter.ToBinary(number, base.length);
   }
}
