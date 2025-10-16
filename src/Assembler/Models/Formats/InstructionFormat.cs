namespace Assembler.Models.Formats;

using OperandType = Assembler.Models.Operands.Operand.Type;

public record InstructionFormat(string opcode, OperandType[] operandTypes, string[] maskSegments)
{
    public string opcode = opcode;
    public OperandType[] operandTypes = operandTypes;
    public string[] maskSegments = maskSegments;

    public int[] GetOperandLengths()
    {
        var lengthMap = new Dictionary<char, int>();
        
        foreach (var segment in maskSegments)
        {
            if (segment.Length >= 2)
            {
                char firstChar = segment[0];
                
                // Check if it's an operand marker (X, Y, Z)
                if (firstChar == 'X' || firstChar == 'Y' || firstChar == 'Z')
                {
                    // Parse the length from the remaining characters (e.g., "X03" -> 3)
                    int length = int.Parse(segment[1..]);
                    lengthMap[firstChar] = length;
                }
            }
        }
        
        // Return lengths in operand order: X, Y, Z
        var result = new List<int>();
        foreach (char c in "XYZ")
        {
            if (lengthMap.ContainsKey(c))
                result.Add(lengthMap[c]);
        }
        
        return result.ToArray();
    } 
}
