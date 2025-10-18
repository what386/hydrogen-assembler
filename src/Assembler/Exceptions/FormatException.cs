namespace Assembler.Exceptions;

/// <summary>
/// Represents errors in formatting, like oversize pages or improper labeling schemes
/// </summary>
public class FormatException : AssemblyException
{
    public FormatException(string message)
        : base($"Assembly formatting error: {message})")
    {
    } 
}
