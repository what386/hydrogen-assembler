namespace Assembler.Exceptions;

// for preprocessor errors, like
// including a missing file
public class DirectiveException : Exception
{
    public DirectiveException(string message) : base(message) { }
}
