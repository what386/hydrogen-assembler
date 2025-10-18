namespace Assembler.Models.Directives;

public class PreprocessorContext
{
    public Stack<bool> ConditionStack { get; } = new();
    public Dictionary<string, string> Definitions { get; } = new();
    public List<string[]> PendingIncludes { get; } = new();
    public HashSet<string> IncludedFiles { get; } = new();
    public string BasePath { get; set; } = "";
}
