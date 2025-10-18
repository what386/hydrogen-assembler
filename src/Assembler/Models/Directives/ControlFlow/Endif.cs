namespace Assembler.Models.Directives.ControlFlow;

public class Endif(PreprocessorContext context) : Directive(context, true)
{       
    public override void Execute()
    {
        context.ConditionStack.Pop();
    }
}
