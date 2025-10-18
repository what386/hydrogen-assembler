namespace Assembler.Exceptions;

/// <summary>
/// Represents errors in assembly syntax, such as missing arguments or incorrect operand types.
/// </summary>
public class SyntaxException : AssemblyException
{
    public int LineNumber { get; }
    public string? Line { get; }
    
    public SyntaxException(string message, string line, int lineNumber)
        : base($"Syntax error at line {lineNumber}: {message} ({line})")
    {
        LineNumber = lineNumber;
        Line = line;
    }

    public SyntaxException(string message) : base(message)
    {
    }
}
