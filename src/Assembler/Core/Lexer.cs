namespace Assembler.Core;

// e.g. tokenizer
using Assembler.Models;
using Assembler.Models.Formats;
using Assembler.Models.Operands;
using OpType = Assembler.Models.Operands.Operand.Type;


public static class Lexer
{
    // inst op1, op2, op3
    public static Instruction ParseLine(string line)
    {
        string[] parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        
        string mnemonicString = parts[0];
        string[] operandStrings = parts.Length > 1 
            ? parts[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            : Array.Empty<string>();
       
        var (mnemonic, bits) = HandleAlias(mnemonicString);
        Operand[] operands = GetOperands(operandStrings);

        return new Instruction(mnemonic,bits,operands);
    }

    private static (string baseop, string? bits) HandleAlias(string mnemonic)
    {
        if(AliasTable.InstructionAliases.TryGetValue(mnemonic, out var value))
            return value;

        return (mnemonic, null);
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
            OpType.IMMEDIATE => new Immediate(value),
            OpType.ADDRESS=> new Address(value),
            OpType.CONDITION=> new Condition(value),
            OpType.REGISTER=> new Register(value),
            OpType.SETTING=> new Setting(value),
            OpType.SPECIALREG=> new SpecialRegister(value),
            _ => throw new ArgumentException($"Invalid operand type in creation of new operand: {type}")
        };
    }

    public static Operand[] GetOperands(string[] stringOperands)
    {
        List<Operand> outputOperands = new();
        
        foreach(var operandString in stringOperands)
        {
            string value = operandString[1..];

            OpType operandType = GetOperandType(operandString);

            outputOperands.Add(CreateOperand(value, operandType));
        }

        return outputOperands.ToArray();
    }
}
