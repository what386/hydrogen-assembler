namespace Assembler.Exceptions;

// for unexpected errors in the assembler
// the user shouldnt see this, and
// if they do something has gone wrong
public class InternalException : Exception
{
    public InternalException(string message)
        : base($"Critical assembler error: {message}")
    {
    }
}
