namespace Assembler.Core;

using Assembler.Exceptions;

using Assembler.Models;
using Assembler.Models.Formats;
using Assembler.Models.Operands;
using OpType = Assembler.Models.Operands.Operand.Type;


public class Lexer
{
    public Instruction[] GetInstructions(string[] lines)
    {
        var instructions = new Instruction[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                instructions[i] = GetInstruction(lines[i]);
            }
            catch (SyntaxException ex)
            {
                throw new SyntaxException(ex.Message, lines[i], i);
            }
        }
        return instructions;
    }

    // inst op1, op2, op3
    public Instruction GetInstruction(string line)
    {
        // Add validation
        if (string.IsNullOrWhiteSpace(line))
            throw new AssemblerException("Lexer attempted to parse empty line");

        string[] parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        
        string mnemonicString = parts[0];
        string[] operandStrings = parts.Length > 1 
            ? parts[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            : Array.Empty<string>();

        foreach (var operand in operandStrings)
        {
            if (operand.Contains(' '))
                throw new SyntaxException($"Operands must be separated by commas. Found space in operand: '{operand}'");
        }
       
        Operand[] operands = GetOperands(operandStrings);

        return new Instruction(mnemonicString, operands);
    }

    private OpType GetNamedType(string operandString)
    {
        if (operandString.Length <= 2 || operandString[0] != '[' || operandString[^1] != ']')
            throw new SyntaxException($"Malformed named operand '{operandString}'"); 
        
        string value = operandString[1..^1];

        if (NameTable.Settings.ContainsKey(value))
            return OpType.SETTING;
        else if (NameTable.SpecialRegisters.ContainsKey(value))
            return OpType.SPECIALREG;
        else
            throw new SyntaxException($"Invalid named operand '{operandString}'");
    }

    private OpType GetOperandType(string operandString)
    {
        char prefix = operandString[0];

        OpType expectedType = prefix switch
        {
            '@' => OpType.ADDRESS,
            'r' => OpType.REGISTER,
            '!' => OpType.IMMEDIATE,
            '?' => OpType.CONDITION,
            '[' => GetNamedType(operandString),
            _ => throw new SyntaxException($"Missing operand prefix for operand '{operandString}'") 
        };

        return expectedType;
    }

    private Operand CreateOperand(string value, OpType type)
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
            _ => throw new AssemblerException($"Invalid operand type in creation of new operand: {type}")
        };
    }

    private Operand[] GetOperands(string[] stringOperands)
    {
        var operands = new Operand[stringOperands.Length];
        for (int i = 0; i < stringOperands.Length; i++)
        {
            OpType operandType = GetOperandType(stringOperands[i]);
            operands[i] = CreateOperand(stringOperands[i], operandType);
        }
        return operands;
    }
}
