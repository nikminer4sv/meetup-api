namespace MeetupAPI.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) 
        : base($"Object with name \"{name}\" ({key}) not found.")
    {
    }
}