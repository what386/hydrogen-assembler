namespace Assembler.Models.Directives.ControlFlow;

public class Else : Directive
{
    public Else(PreprocessorContext context) : base(context)
    {
        this.ignoresActive = true;
    }

    public override void Execute()
    {
        bool current = context.ConditionStack.Pop();
        context.ConditionStack.Push(!current);
    }
}
