namespace Assembler.Models.Directives.ControlFlow;

public class Else(PreprocessorContext context) : Directive(context, true)
{
    public override void Execute()
    {
        bool current = context.ConditionStack.Pop();
        context.ConditionStack.Push(!current);
    }
}
