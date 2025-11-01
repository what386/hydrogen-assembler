namespace Assembler.Models.Directives.ControlFlow;

using Assembler.Exceptions;

public class Else : Directive
{
    public Else(PreprocessorContext context) : base(context)
    {
        this.ignoresActive = true;
    }

    public override void Execute()
    {
        if (context.ConditionStack.Count < 1)
            throw new DirectiveException("Invalid else position");

        bool current = context.ConditionStack.Pop();
        context.ConditionStack.Push(!current);
    }
}
