namespace Assembler.Models.Directives.ControlFlow;

public class Endif : Directive
{
    public Endif(PreprocessorContext context) : base(context)
    {
        this.ignoresActive = true;
    }
    
    public override void Execute()
    {
        context.ConditionStack.Pop();
    }
}
