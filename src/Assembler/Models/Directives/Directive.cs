namespace Assembler.Models.Directives;

public abstract class Directive
{
    protected PreprocessorContext context;

    public bool ignoresActive {get; protected set;} = false;
    public bool needsDefinitions {get; protected set;} = false;
    
    protected Directive(PreprocessorContext context)
    {
        this.context = context;
    }

    public abstract void Execute();
}
