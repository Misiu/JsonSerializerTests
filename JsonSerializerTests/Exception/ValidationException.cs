namespace JsonSerializerTests.Exception;

public sealed class ValidationException : BaseException
{
    public ValidationException(string message)
        : this(string.Empty, message)
    {
    }

    public ValidationException(string? field, string message)
        : this(new List<ValidationExceptionDetails> { new() { Field = field ?? string.Empty, Message = message } })
    {
    }

    public ValidationException(IEnumerable<ValidationExceptionDetails> details)
    {
        Details = details.ToList();
        Description = "Errors occurred during validation";
    }
}