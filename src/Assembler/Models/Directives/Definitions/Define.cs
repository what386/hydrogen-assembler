namespace Assembler.Models.Directives.Definitions;

using Assembler.Exceptions;

public class Define : Directive
{
    string operand;

    public Define(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new DirectiveException("Definition argument cannot be empty");

        this.operand = operand;
    }

    public override void Execute()
    {
        string[] parts = operand.Split(new[]{' '}, 2);
        
        if (parts.Length == 2)
            context.Definitions[parts[0]] = parts[1];
        else
            throw new DirectiveException($"Invalid definition: {operand}");
    }
}
