namespace Assembler.Exceptions;

// for errors in syntax, such as missing 
// arguments or incorrect operand types
public class SyntaxException : AssemblyException
{
    public int LineNumber { get; }
    public string Line { get; }
    public SyntaxException(string message, string line, int lineNumber)
        : base($"Syntax error at line {lineNumber}: {line} ({message})")
    {
        LineNumber = lineNumber;
        Line = line;
    }
}
