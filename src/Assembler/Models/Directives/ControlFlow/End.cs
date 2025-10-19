namespace Assembler.Models.Directives.ControlFlow;

public class End : Directive
{
    public End(PreprocessorContext context) : base(context)
    {
        this.ignoresActive = true;
    }
    // This class does nothing on its own.
    // It serves as a maker for other directives 
    // to stop gathering lines.
    public override void Execute()
    {
    }
}
