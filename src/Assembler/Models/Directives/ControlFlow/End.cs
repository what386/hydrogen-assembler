namespace Assembler.Models.Directives.ControlFlow;

public class End(PreprocessorContext context) : Directive(context, true)
{
    // This class does nothing on its own.
    // It serves as a maker for other directives 
    // to stop gathering lines.
    public override void Execute()
    {
    }
}
