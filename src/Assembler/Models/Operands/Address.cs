namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Address(string value) : Operand(value, LENGTH)
{
   const int LENGTH = 8;

   protected override string Parse()
   {
      if(base.value[..1] != "!")
         throw new System.ArgumentException();

      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, base.length);
   }
}
