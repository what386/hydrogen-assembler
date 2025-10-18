namespace Assembler.Exceptions;

/// <summary>
/// Represents a critical internal error in the assembler that indicates a bug or invalid state.
/// This should never occur during normal operation with valid or invalid input.
/// </summary>
public class AssemblerException : Exception
{
    public AssemblerException(string message) 
        : base($"INTERNAL ASSEMBLER ERROR: {message}. This indicates a bug in the assembler - report if possible.")
    {
    }
    
    public AssemblerException(string message, Exception innerException)
        : base($"INTERNAL ASSEMBLER ERROR: {message}. This indicates a bug in the assembler - report if possible.", innerException)
    {
    }
}
