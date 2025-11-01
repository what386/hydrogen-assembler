namespace Assembler.Models.Directives.ControlFlow;

using Assembler.Exceptions;

public class Ifdef : Directive
{
    string operand;

    public Ifdef(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new DirectiveException("Ifdef argument cannot be empty");

        this.operand = operand;
    }

    public override void Execute()
    {
        context.ConditionStack.Push(context.Definitions.ContainsKey(operand!));
    }
}
