namespace Assembler.Models.Operands;

public abstract class Operand
{
   protected string value;
   protected int length; 
   protected Type type;
   
   public Operand(string value, int length, Type type, bool parse = true)
   {
      this.value = value;
      this.length = length;
      this.type = type;
   }

   public enum Type{
      NONE,
      REGISTER,
      IMMEDIATE,
      ADDRESS,
      CONDITION,
      SETTING,
      SPECIALREG,
      LABEL
   }

   public abstract string Parse();
}

