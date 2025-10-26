namespace Assembler.Utils;

class TextUtils
{
    public static Dictionary<string, byte> BuildAsciiDictionary()
    {
        var dict = new Dictionary<string, byte>();

        // Basic printable characters
        for (char c = (char)32; c < 127; c++)
        {
            dict[c.ToString()] = AddParityBit((byte)c);
        }

        // Common control characters (escaped forms)
        dict["\\0"] = AddParityBit(0);   // Null
        dict["\\a"] = AddParityBit(7);   // Bell
        dict["\\b"] = AddParityBit(8);   // Backspace
        dict["\\t"] = AddParityBit(9);   // Tab
        dict["\\n"] = AddParityBit(10);  // Line Feed
        dict["\\v"] = AddParityBit(11);  // Vertical Tab
        dict["\\f"] = AddParityBit(12);  // Form Feed
        dict["\\r"] = AddParityBit(13);  // Carriage Return
        dict["\\e"] = AddParityBit(27);  // Escape (nonstandard, but common in terminals)
        dict["\\x7F"] = AddParityBit(127); // DEL

        return dict;
    }

    // Adds an even parity bit as the 8th bit
    static byte AddParityBit(byte ascii7bit)
    {
        int bitCount = CountBits(ascii7bit);
        int parityBit = (bitCount % 2 == 0) ? 0 : 1; // even parity
        return (byte)((parityBit << 7) | ascii7bit);
    }

    static int CountBits(byte b)
    {
        int count = 0;
        while (b != 0)
        {
            count += b & 1;
            b >>= 1;
        }
        return count;
    }
}
