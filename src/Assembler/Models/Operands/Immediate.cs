namespace Assembler.Models.Operands;

using Assembler.Exceptions;
using Assembler.Utils;

public class Immediate(string value, int? length = null) : Operand(value, length, Type.IMMEDIATE)
{
   public void SetLength(int length)
    {
        if (this.length != null)
            throw new InvalidOperationException("Length has already been set!");

        this.length = length;
    }

   public override string Parse()
   {
      if (length == null)
         throw new InternalException("Unable to parse immediate with null length!");

      int number = BaseConverter.ToInteger(base.value);
      return BaseConverter.ToBinary(number, (int)base.length);
   }
}
