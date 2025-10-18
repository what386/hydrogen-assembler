namespace Assembler.Exceptions;

/// <summary>
/// Represents errors in directives, like importing an invalid file or a missing label 
/// </summary>
public class DirectiveException : AssemblyException
{
    public int LineNumber { get; }
    public string Line { get; }

    public DirectiveException(string message, string line = "unknown", int lineNumber = -1)
        : base($"Directive error at line {lineNumber}: {message} ({line})")
    {
        LineNumber = lineNumber;
        Line = line;
    } 
}
