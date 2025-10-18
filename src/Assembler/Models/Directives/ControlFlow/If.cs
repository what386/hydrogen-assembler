namespace Assembler.Models.Directives.ControlFlow;

public class If : Directive
{
    string operand;

    public If(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
    }

    private bool EvaluateCondition(string condition)
    {
        // Use VB-style syntax for DataTable.Compute
        condition = condition
            .Replace("==", "=")
            .Replace("!=", "<>")
            .Replace("&&", "AND")
            .Replace("||", "OR")
            .Replace("!", "NOT ");

        try
        {
            var dt = new System.Data.DataTable();
            var result = dt.Compute(condition, "");
            return Convert.ToBoolean(result);
        }
        catch
        {
            throw new ArgumentException();
        }
    }

    public override void Execute()
    {
        bool result = EvaluateCondition(operand!);
        context.ConditionStack.Push(result);
    }
}
