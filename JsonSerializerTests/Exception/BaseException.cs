namespace JsonSerializerTests.Exception;

public abstract class BaseException : System.Exception
{
    protected BaseException()
    {
        Type = GetType().Name;
    }

    public string Type { get; }

    public object? Details { get; protected set; }

    public string? Description { get; protected set; }

    public override string Message => $"Type: {Type}{Environment.NewLine}Details: {Details}{Environment.NewLine}Description: {Description}{Environment.NewLine}Message: {base.Message}";
}