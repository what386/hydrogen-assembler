namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Immediate(string name, int? length = null) : Operand(name, length ?? 0, Type.IMMEDIATE)
{
   public override string Parse()
   {
      int number = BaseConverter.ToInteger(value);
         
      if(number > ((1 << base.length) - 1))
         throw new ArgumentException();

      return BaseConverter.ToBinary(number, base.length);
   }
}
