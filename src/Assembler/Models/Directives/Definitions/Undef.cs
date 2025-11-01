namespace Assembler.Models.Directives.Definitions;

using Assembler.Exceptions;

public class Undef : Directive
{
    string operand;

    public Undef(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new DirectiveException("Undefinition argument cannot be empty");

        this.operand = operand;
    }

    public override void Execute()
    {
        if (!operand.Contains(' '))
            context.Definitions.Remove(operand);
        else
            throw new DirectiveException($"Invalid undefinition: {operand}");
    }
}
