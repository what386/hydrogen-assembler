namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Immediate(string value, int length) : Operand(value, length)
{

   protected override string Parse()
   {
      if(base.value[..1] != "!")
         throw new System.ArgumentException();

      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, base.length);
   }
}
