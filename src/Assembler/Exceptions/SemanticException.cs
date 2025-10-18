namespace Assembler.Exceptions;

/// <summary>
/// Represents errors in semantics, like incorrect operand order or jumping to an invalid address
/// </summary>
public class SemanticException : AssemblyException
{
    public int LineNumber { get; }
    public string? Line { get; }

    public SemanticException(string message, string line, int lineNumber)
        : base($"Semantic error at line {lineNumber}: {message} ({line})")
    {
        LineNumber = lineNumber;
        Line = line;
    } 

    public SemanticException(string message) : base(message)
    {
    }
}
