namespace JsonSerializerTests.Exception;

public sealed class ValidationExceptionDetails
{
    public string Field { get; set; } = null!;

    public string Message { get; set; } = null!;
}