namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Immediate(string value, int length) : Operand(value, length, Type.IMMEDIATE)
{
   public override string Parse()
   {
      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, length);
   }
}
