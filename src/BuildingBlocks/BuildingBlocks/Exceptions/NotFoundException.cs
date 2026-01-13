namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string name, object obj) : base($"Entity \"{name}\" ({obj}) was not found.")
    {
    }
}
