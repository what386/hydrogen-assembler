namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Immediate(string value, int length) : Operand(value, length, Type.IMMEDIATE)
{
   protected override string Parse()
   {
      if(base.value[..1] != "!")
         throw new ArgumentException($"Invalid prefix for immediate: '{value}'");

      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, base.length);
   }
}
