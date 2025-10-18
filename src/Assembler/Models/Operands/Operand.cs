namespace Assembler.Models.Operands;

public abstract class Operand
{
   public string name;
   public string value;
   public int length; 
   public Type type;
   
   private static string RemovePrefixIfExists(string value)
   {
      if (string.IsNullOrEmpty(value))
         return value;

      char firstChar = value[0];

      // Check if first character is a known prefix
      if ("!@r?.".Contains(firstChar))
         return value[1..];

      if (firstChar.Equals('['))
         return value[1..^1];

      return value;
   }

   public Operand(string name, int length, Type type)
   {
      this.name = name;
      this.value=RemovePrefixIfExists(name);
      this.length = length;
      this.type = type;
   }

   public enum Type{
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

