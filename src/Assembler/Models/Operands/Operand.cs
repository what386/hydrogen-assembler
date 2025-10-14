namespace Assembler.Models.Operands;

public abstract class Operand
{
   protected string value;
   protected int length; 
   
   protected string? binary = null;

   public string? Binary => binary;
  
   public Operand(string value, int length, bool parse = true)
   {
      this.value = value;
      this.length = length;

      if (parse)
         binary = Parse();
   }

   protected abstract string Parse();
}
