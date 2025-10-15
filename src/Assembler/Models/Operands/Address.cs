namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Address(string value) : Operand(value, LENGTH, Type.ADDRESS)
{
   const int LENGTH = 8;

   protected override string Parse()
   {
      if(base.value[..1] != "!")
         throw new ArgumentException($"Invalid prefix for address: '{value}'");

      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, base.length);
   }
}
