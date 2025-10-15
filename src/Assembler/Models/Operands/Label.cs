namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Label(string value, string? destination) : Operand(value, LENGTH, Type.LABEL)
{
   const int LENGTH = 16;

   public void SetValue(int length)
    {
        if (this.length != null)
            throw new InvalidOperationException("Length has already been set!");

        this.length = length;
    }

   public override string Parse()
   {
      if (destination == null)
         throw new ArgumentException("Unable to parse label with null destination!");

      int number = BaseConverter.ToInteger(destination);
      return BaseConverter.ToBinary(number, LENGTH);
   }
}
