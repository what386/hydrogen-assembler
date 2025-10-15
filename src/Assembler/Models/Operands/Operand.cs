namespace Assembler.Models.Operands;

public abstract class Operand
{
   protected string value;
   protected int length; 
   protected Type type;
   
   protected string? binary = null;

   public string? Binary => binary;
  
   public Operand(string value, int length, Type type, bool parse = true)
   {
      this.value = value;
      this.length = length;
      this.type = type;

      if (parse)
         binary = Parse();
   }

   public enum Type{
      NONE,
      REGISTER,
      IMMEDIATE,
      ADDRESS,
      CONDITION,
      SETTING,
      SPECIALREG
   }

   protected abstract string Parse();
}

