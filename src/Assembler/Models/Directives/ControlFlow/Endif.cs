namespace Assembler.Models.Directives.ControlFlow;

using Assembler.Exceptions;

public class Endif : Directive
{
    public Endif(PreprocessorContext context) : base(context)
    {
        this.ignoresActive = true;
    }
    
    public override void Execute()
    {
        if(context.ConditionStack.Count < 1)
            throw new DirectiveException("Invalid endif position");

        context.ConditionStack.Pop();
    }
}
