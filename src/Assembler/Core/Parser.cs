namespace Assembler.Core;

using System.Text;

using Assembler.Models;
using Assembler.Models.Operands;
using Assembler.Models.Formats;

using Assembler.Exceptions;

public class Parser
{
    public string[] ParseInstructions(Instruction[] instructions)
    {
        var binary = new string[instructions.Length];
        for (int i = 0; i < instructions.Length; i++)
        {
            try
            {
                binary[i] = ParseInstruction(instructions[i]);
            }
            catch (SemanticException ex)
            {
                throw new SemanticException(ex.Message, instructions[i].ToString(), i);
            }
        }
        return binary;
    }

    public string ParseInstruction(Instruction instruction)
    {
        string alias = instruction.mnemonic;
        Operand[] operands = instruction.operands;
        
        var (mnemonic, typeBits) = GetInstructionAlias(alias);
        InstructionFormat format = GetInstructionFormat(mnemonic);
        
        int[] operandLengths = format.GetOperandLengths();

        if (operands.Length != operandLengths.Length)
            throw new SemanticException($"Instruction '{mnemonic}' expects {operandLengths.Length} operands, got {operands.Length}");

        for (int i = 0; i < operands.Length; i++)
        {
            if (operands[i].type != format.operandTypes[i])
                throw new SemanticException($"Instruction '{mnemonic}' recieved incorrect operand in position {i}: {operands[i]}");
        }

        SetOperandLengths(operands, operandLengths);
        string[] operandBinaries = ParseOperandsToBinary(operands, operandLengths);
        ValidateTypeBits(typeBits, format);
        
        return AssembleBinaryInstruction(format, operandBinaries, typeBits);
    }

    private (string mnemonic, string? typeBits) GetInstructionAlias(string alias)
    {
        if (AliasTable.InstructionAliases.TryGetValue(alias, out var result))
            return result;
        
        return (alias, null);
    } 

    private InstructionFormat GetInstructionFormat(string mnemonic)
    {
        if (!InstructionTable.Formats.ContainsKey(mnemonic))
            throw new SemanticException($"Unknown instruction: {mnemonic}");
        
        return InstructionTable.Formats[mnemonic];
    }

    private void SetOperandLengths(Operand[] operands, int[] operandLengths)
    {
        if(operandLengths.Length != operands.Length)
            throw new SemanticException($"Invalid operandlengths ({operandLengths.Length}) for operands: ({operands.Length})");

        for (int i = 0; i < operands.Length; i++)
        {
            if (operands[i].type == Operand.Type.IMMEDIATE)
                operands[i].length = operandLengths[i];
        }
    }

    private string[] ParseOperandsToBinary(Operand[] operands, int[] operandLengths)
    {
        string[] operandBinaries = new string[operands.Length];
        
        for (int i = 0; i < operands.Length; i++)
        {
            operandBinaries[i] = operands[i].Parse();
            
            if (operandBinaries[i].Length != operandLengths[i])
                throw new SemanticException(
                    $"Operand {i} expected {operandLengths[i]} bits, got {operandBinaries[i].Length} bits ({operandBinaries[i]})");
        }
        
        return operandBinaries;
    }

    private void ValidateTypeBits(string? typeBits, InstructionFormat format)
    {
        if (typeBits == null)
            return;
        
        int typeBitsLength = GetTypeBitsLength(format.maskSegments);
        
        if (typeBits.Length != typeBitsLength)
            throw new SemanticException(
                $"Type bits length mismatch: expected {typeBitsLength} bits, got {typeBits.Length} bits");
    }

    private string AssembleBinaryInstruction(InstructionFormat format, string[] operandBinaries, string? typeBits)
    {
        var binary = new StringBuilder(format.opcode);
        
        foreach (var segment in format.maskSegments)
        {
            // Handle literal segments
            if (!char.IsLetter(segment[0]))
            {
                binary.Append(segment);
                continue;
            }

            // Handle marker segments
            char marker = segment[0];
            switch (marker)
            {
                case 'T':
                    if (typeBits == null)
                        throw new AssemblerException($"Type bits required but not provided: {segment}");
                    binary.Append(typeBits);
                    break;
                    
                case 'X':
                case 'Y':
                case 'Z':
                    int operandIndex = GetOperandIndex(marker);
                    if (operandIndex >= operandBinaries.Length)
                        throw new SemanticException($"Operand {marker} referenced but not provided");

                    binary.Append(operandBinaries[operandIndex]);
                    break;
                    
                default:
                    throw new AssemblerException($"Invalid segment marker: {marker}");
            } 
        }
        
        return binary.ToString();
    }

    private int GetOperandIndex(char marker)
    {
        return marker switch
        {
            'X' => 0,
            'Y' => 1,
            'Z' => 2,
            _ => throw new AssemblerException($"Invalid operand marker: {marker}")
        };
    }

    private int GetTypeBitsLength(string[] maskSegments)
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
}
