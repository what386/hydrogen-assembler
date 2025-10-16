namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Register(string value) : Operand(value, LENGTH, Type.REGISTER)
{
   const int LENGTH = 3;

   private int GetIndex(string register)
   {
      if(!int.TryParse(register, out int index))
         throw new ArgumentException($"Invalid register index: '{register}'");

      int maxIndex = (1 << LENGTH) - 1; // 2^length-1

      if(index < 0 || index > maxIndex)
         throw new ArgumentOutOfRangeException($"Register index '{index}' is out of range.");

      return index;
   } 

   public override string Parse()
   {
      return BaseConverter.ToBinary(GetIndex(value), base.length);
   }
}
