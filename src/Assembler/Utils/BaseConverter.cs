namespace Assembler.Utils;

public static class BaseConverter
{
    public static int ToInteger(string input)
    {
        if (input.Length < 2 || !input.StartsWith("0"))
            return Convert.ToInt32(input, 10);  // Decimal
        
        string prefix = input[..2].ToLower();
        string number = input[2..];
        
        return prefix switch
        {
            "0x" => Convert.ToInt32(number, 16),  // Hexadecimal
            "0b" => Convert.ToInt32(number, 2),   // Binary
            "0o" => Convert.ToInt32(number, 8),   // Octal
            "0d" => Convert.ToInt32(number, 10),   // Decimal 
            _ => throw new System.ArgumentException()
        };
    } 

    public static string ToBinary(int number, int length, bool signed = false)
    {
        if (length is <= 0 or > 32)
            throw new ArgumentOutOfRangeException("Invalid length");

        int maxUnsigned = (1 << length) - 1;        // 2^length - 1
        int minSigned = -(1 << (length - 1));       // -2^(length-1)
        int maxSigned = (1 << (length - 1)) - 1;    // 2^(length-1) - 1
        
        if (signed)
        {
            if (number < minSigned || number > maxSigned)
                throw new ArgumentOutOfRangeException();
        }
        else
        {
            if (number < 0 || number > maxUnsigned)
                throw new ArgumentOutOfRangeException();
        }
        
        int mask = (1 << length) - 1;
        
        // For negative numbers in signed mode, use two's complement
        int value = number & mask;
        
        string binary = Convert.ToString(value, 2).PadLeft(length, '0');
        
        return $"0b{binary}";
    }
}
