namespace Assembler.Exceptions;

/// <summary>
/// Represents errors in directives, like importing an invalid file or a missing label 
/// </summary>
public class DirectiveException : AssemblyException
{
    public int LineNumber { get; }
    public string? Line { get; }

    public DirectiveException(string message, string line, int lineNumber)
        : base($"Directive error at line {lineNumber}: {message} ({line})")
    {
        LineNumber = lineNumber;
        Line = line;
    } 

    public DirectiveException(string message) : base(message)
    {
    }
}
