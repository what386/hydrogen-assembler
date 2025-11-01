namespace Assembler.Models.Directives.ControlFlow;

using Assembler.Exceptions;

public class If : Directive
{
    string operand;

    public If(PreprocessorContext context, string operand) : base(context)
    {
        if (String.IsNullOrEmpty(operand))
            throw new ArgumentException();

        this.operand = operand;
        this.needsDefinitions = true;
    }

    private bool EvaluateCondition(string condition)
    {
        condition = condition.Replace("!=", "<>");

        try
        {
            var dt = new System.Data.DataTable();
            var result = dt.Compute(condition, "");
            return Convert.ToBoolean(result);
        }
        catch
        {
            throw new DirectiveException($"Invalid conditional: {condition}");
        }
    }

    public override void Execute()
    {
        bool result = EvaluateCondition(operand!);
        context.ConditionStack.Push(result);
    }
}
