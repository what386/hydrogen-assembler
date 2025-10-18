namespace Assembler.Models.Directives.Definitions;

public class Define : Directive
{
    string operand;

    public Define(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
    }

    public override void Execute()
    {
        string[] parts = operand.Split(new[]{' '}, 2);
        
        if (parts.Length == 2)
            context.Definitions[parts[0]] = parts[1];
        else
            throw new ArgumentException();
    }
}
