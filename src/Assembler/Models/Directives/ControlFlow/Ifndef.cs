namespace Assembler.Models.Directives.ControlFlow;

using Assembler.Exceptions;

public class Ifndef : Directive
{
    string operand;

    public Ifndef(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new DirectiveException("Ifdef argument cannot be empty");

        this.operand = operand;
    }

    public override void Execute()
    {
        context.ConditionStack.Push(!context.Definitions.ContainsKey(operand!));
    }
}
