namespace Assembler.Models.Directives.Transforms;

public class Include : Directive
{
    string operand;

    public Include(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
    }

    public override void Execute()
    {
        var file = operand.Trim('"');
        var fullPath = Path.Combine(context.BasePath, file);
       
        if (!context.IncludedFiles.Contains(fullPath))
        {
            context.IncludedFiles.Add(fullPath);
            string[] includedFileLines = File.ReadAllLines(fullPath);
            context.PendingIncludes.Add(includedFileLines);
        } 
    }
}
