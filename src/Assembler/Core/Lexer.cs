namespace Assembler.Core;

// e.g. tokenizer
using Assembler.Models;
using Assembler.Models.Formats;
using Assembler.Models.Operands;
using OpType = Assembler.Models.Operands.Operand.Type;


public static class Lexer
{
    public static Instruction[] ParseAllLines(string[] lines)
    {
        return lines.Select(GetInstruction).ToArray();
    } 

    // inst op1, op2, op3
    public static Instruction GetInstruction(string line)
    {
        string[] parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        
        string mnemonicString = parts[0];
        string[] operandStrings = parts.Length > 1 
            ? parts[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            : Array.Empty<string>();
       
        Operand[] operands = GetOperands(operandStrings);

        return new Instruction(mnemonicString, operands);
    }

    private static OpType GetNamedType(string operandString)
    {
        if (operandString.Length < 3 || operandString[0] != '[' || operandString[^1] != ']')
        throw new ArgumentException($"Malformed named operand: {operandString}");

        string value = operandString[1..^1];

        if (NameTable.Settings.ContainsKey(value))
            return OpType.SETTING;
        else if (NameTable.SpecialRegisters.ContainsKey(value))
            return OpType.SPECIALREG;
        else
            throw new ArgumentException();
    }

    private static OpType GetOperandType(string operandString)
    {
        char prefix = operandString[0];

        OpType expectedType = prefix switch
        {
            '@' => OpType.ADDRESS,
            'r' => OpType.REGISTER,
            '!' => OpType.IMMEDIATE,
            '?' => OpType.CONDITION,
            '.' => OpType.LABEL,
            '[' => GetNamedType(operandString),
            _ => throw new ArgumentException($"Malformed operand: {operandString}")
        };

        return expectedType;
    }

    private static Operand CreateOperand(string value, OpType type)
    {
        return type switch
        {
            // imm defaults to "0" length if not provided
            OpType.IMMEDIATE => new Immediate(value),
            OpType.ADDRESS => new Address(value),
            OpType.CONDITION => new Condition(value),
            OpType.REGISTER => new Register(value),
            OpType.SETTING => new Setting(value),
            OpType.SPECIALREG => new SpecialRegister(value),
            _ => throw new ArgumentException($"Invalid operand type in creation of new operand: {type}")
        };
    }

    private static Operand[] GetOperands(string[] stringOperands)
    {
        List<Operand> outputOperands = new();
        
        foreach(var operandString in stringOperands)
        {
            OpType operandType = GetOperandType(operandString);
            outputOperands.Add(CreateOperand(operandString, operandType));
        }

        return outputOperands.ToArray();
    }
}
