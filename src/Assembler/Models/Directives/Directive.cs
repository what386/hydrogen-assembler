namespace Assembler.Models.Directives;

public abstract class Directive
{
    protected PreprocessorContext context;

    public readonly bool ignoresActive;
    
    protected Directive(PreprocessorContext context, bool ignoresActive = false)
    {
        this.context = context;
        this.ignoresActive = ignoresActive;
    }

    public abstract void Execute();
}
