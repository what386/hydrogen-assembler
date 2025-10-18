namespace Assembler.Models.Directives.ControlFlow;

public class Ifdef : Directive
{
    string operand;

    public Ifdef(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
    }

    public override void Execute()
    {
        context.ConditionStack.Push(context.Definitions.ContainsKey(operand!));
    }
}
