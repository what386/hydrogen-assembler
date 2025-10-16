namespace Assembler.Core;
using Assembler.Models;
using Assembler.Models.Operands;
using Assembler.Models.Formats;

public static class Parser
{
    public static string ParseInstruction(Instruction instruction)
    {
        string alias = instruction.mnemonic;
        Operand[] operands = instruction.operands;
        
        var (mnemonic, typeBits) = GetInstructionAlias(alias);
        InstructionFormat format = GetInstructionFormat(mnemonic);
        
        SetOperandLengths(operands, format);
        string[] operandBinaries = ParseOperandsToBinary(operands, format);
        ValidateTypeBits(typeBits, format);
        
        return AssembleBinaryInstruction(format, operandBinaries, typeBits);
    }

    private static (string mnemonic, string? typeBits) GetInstructionAlias(string alias)
    {
        if (!AliasTable.InstructionAliases.ContainsKey(alias))
            return (alias, null);
        
        return AliasTable.InstructionAliases[alias];
    }

    private static InstructionFormat GetInstructionFormat(string mnemonic)
    {
        if (!InstructionTable.Formats.ContainsKey(mnemonic))
            throw new ArgumentException($"Unknown instruction: {mnemonic}");
        
        return InstructionTable.Formats[mnemonic];
    }

    private static void SetOperandLengths(Operand[] operands, InstructionFormat format)
    {
        int[] operandLengths = format.GetOperandLengths();
        
        for (int i = 0; i < operands.Length; i++)
        {
            if (operands[i].type == Operand.Type.IMMEDIATE)
                operands[i].length = operandLengths[i];
        }
    }

    private static string[] ParseOperandsToBinary(Operand[] operands, InstructionFormat format)
    {
        int[] operandLengths = format.GetOperandLengths();
        string[] operandBinaries = new string[operands.Length];
        
        for (int i = 0; i < operands.Length; i++)
        {
            operandBinaries[i] = operands[i].Parse();
            
            if (operandBinaries[i].Length != operandLengths[i])
                throw new ArgumentException(
                    $"Operand {i} length mismatch: expected {operandLengths[i]} bits, got {operandBinaries[i].Length} bits ({operandBinaries[i]})");
        }
        
        return operandBinaries;
    }

    private static void ValidateTypeBits(string? typeBits, InstructionFormat format)
    {
        if (typeBits == null)
            return;
        
        int typeBitsLength = GetTypeBitsLength(format.maskSegments);
        
        if (typeBits.Length != typeBitsLength)
            throw new ArgumentException(
                $"Type bits length mismatch: expected {typeBitsLength} bits, got {typeBits.Length} bits");
    }

    private static string AssembleBinaryInstruction(InstructionFormat format, string[] operandBinaries, string? typeBits)
    {
        string binary = format.opcode;
        
        foreach (var segment in format.maskSegments)
        {
            if (segment.Length >= 2 && char.IsLetter(segment[0]))
            {
                char marker = segment[0];
                
                if (marker == 'T')
                {
                    binary += typeBits ?? throw new ArgumentException("Type bits required but not provided");
                }
                else if (marker == 'X' || marker == 'Y' || marker == 'Z')
                {
                    int operandIndex = GetOperandIndex(marker);
                    
                    if (operandIndex >= operandBinaries.Length)
                        throw new ArgumentException($"Operand {marker} referenced but not provided");
                    
                    binary += operandBinaries[operandIndex];
                }
            }
            else
            {
                binary += segment;
            }
        }
        
        return binary;
    }

    private static int GetOperandIndex(char marker)
    {
        return marker switch
        {
            'X' => 0,
            'Y' => 1,
            'Z' => 2,
            _ => throw new ArgumentException($"Invalid operand marker: {marker}")
        };
    }

    private static int GetTypeBitsLength(string[] maskSegments)
    {
        foreach (var segment in maskSegments)
        {
            if (segment.Length >= 2 && segment[0] == 'T')
            {
                return int.Parse(segment[1..]);
            }
        }
        return 0;
    }

    private static (string baseop, string? bits) HandleAlias(string mnemonic)
    {
        if (AliasTable.InstructionAliases.TryGetValue(mnemonic, out var value))
            return value;
        return (mnemonic, null);
    }
}
