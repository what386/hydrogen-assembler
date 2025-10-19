namespace Assembler.Models.Directives.ControlFlow;

public class Ifndef : Directive
{
    string operand;

    public Ifndef(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
    }

    public override void Execute()
    {
        context.ConditionStack.Push(!context.Definitions.ContainsKey(operand!));
    }
}
