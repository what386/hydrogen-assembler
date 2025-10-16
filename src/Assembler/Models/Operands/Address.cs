namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Address(string name) : Operand(name, LENGTH, Type.ADDRESS)
{
   const int LENGTH = 8;

   public override string Parse()
   {
      int number = BaseConverter.ToInteger(value);
         
      if(number > ((1 << LENGTH) - 1))
         throw new ArgumentException();

      return BaseConverter.ToBinary(number, base.length);
   }
}
