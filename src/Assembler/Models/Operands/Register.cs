namespace Assembler.Models.Operands;

using Assembler.Utils;

public class Register(string value) : Operand(value, LENGTH, Type.REGISTER)
{
   const int LENGTH = 3;

   private int GetIndex(string register)
   {
      if(register.Length is < 2)
         throw new ArgumentException($"Invalid register format: '{register}'");

      string indexStr = register[1..];

      if(!int.TryParse(indexStr, out int index))
         throw new ArgumentException($"Invalid register index: '{indexStr}'");

      int maxIndex = (1 << LENGTH) - 1;

      if(index < 0 || index > maxIndex)
         throw new ArgumentOutOfRangeException($"Register index '{index}' is out of range.");

      return index;
   } 

   protected override string Parse()
   {
      if(base.value[..1].ToLower() != "r")
         throw new ArgumentException($"Argument is not a register: '{base.value}'");


      int index = GetIndex(base.value);
      return BaseConverter.ToBinary(index, base.length);
   }
}
