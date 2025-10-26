namespace Assembler.Models.Operands;

using Assembler.Exceptions;
using Assembler.Utils;

public class Character(string name) : Operand(name, LENGTH, Type.CHARACTER)
{
   const int LENGTH = 3;

   public override string Parse()
   {
      Dictionary<string, byte> AsciiTable = TextUtils.BuildAsciiDictionary();

      if(AsciiTable.TryGetValue(base.value, out var result))
         return BaseConverter.ToBinary(result, 8);

      throw new SemanticException($"Character not found in Ascii table: '{base.value}'");
   } 
}
