namespace Assembler.Exceptions;

// for errors in semantics, like duplicate
// labels or jumping to an invalid address
public class SemanticException : AssemblyException
{
    public int LineNumber { get; }
    public string Line { get; }
    public SemanticException(string message, string line, int lineNumber)
        : base($"Semantic error at line {lineNumber}: {line} ({message})")
    {
        LineNumber = lineNumber;
        Line = line;
    } 
}
