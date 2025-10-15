namespace Assembler.Exceptions;

public abstract class AssemblyException : Exception
{
    protected AssemblyException(string message) : base(message) { }
}
