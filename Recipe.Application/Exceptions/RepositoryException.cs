namespace Recipe.Application.Exceptions;

public class RepositoryException(string code, string message, string description) : Exception(message)
{
    public string Code { get; } = code;
    public DateTime Date { get; } = DateTime.Now;
    public string Description { get; } = description;
}