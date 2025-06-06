using Recipe.Application.Exceptions;

namespace Recipe.Infrastructure.Handler;

public class ErrorHandler
{
    public static void NotFound(string entityName, object key)
    {
        throw new RepositoryException(
            "404",
            $"{entityName} not found.",
            $"No {entityName.ToLower()} found with ID {key}."
        );
    }

    public static void BadRequest(string message, string description)
    {
        throw new RepositoryException(
            "400",
            message,
            description
        );
    }

    public static void Unauthorized(string description = "Access is denied.")
    {
        throw new RepositoryException(
            "401",
            "Unauthorized",
            description
        );
    }

    public static void AlreadyExists(string entity, string field, object value)
    {
        throw new RepositoryException(
            "400", 
            $"{entity} with {field} '{value}' already exists.", 
            $"{entity}Conflict");
    }
}