namespace Assembler.Models.Directives.Transforms;

using Assembler.Exceptions;

public class Include : Directive
{
    string operand;

    public Include(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new DirectiveException("Include argument cannot be empty");

        this.operand = operand;
    }

    public override void Execute()
    {
        var file = operand.Trim('"');
        var fullPath = Path.Combine(context.BasePath, file);

        if (!File.Exists(fullPath))
            throw new DirectiveException($"Included file does not exist: {fullPath}");
       
        if (!context.IncludedFiles.Contains(fullPath))
        {
            context.IncludedFiles.Add(fullPath);
            string[] includedFileLines = File.ReadAllLines(fullPath);
            context.PendingIncludes.Add(includedFileLines);
        } 
    }
}
