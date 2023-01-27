using JsonSerializerTests;
using JsonSerializerTests.Exception;
using Xunit;

namespace TestProject1;

public sealed class Tests
{
    public static readonly object[][] CorrectData =
    {
        new object[] { new ValidationException("TEST")},
        new object[] { new ArgumentException("TEST")},
        new object[] { new ApplicationException("TEST")},
    };

    [Theory, MemberData(nameof(CorrectData))]
    public void WhenException__CreatesProperties(System.Exception exception)
    {
        var baseExceptionContractResolver = new BaseExceptionContractResolver();
        
        var properties1 = Newtonsoft.Json.JsonConvert.SerializeObject(exception, new Newtonsoft.Json.JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented, ContractResolver = baseExceptionContractResolver });

        var properties2 = System.Text.Json.JsonSerializer.Serialize(exception, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver
            {
                Modifiers = { Modifiers.OnlyBaseExceptionProperties }
            }
        });
        
        Assert.NotNull(properties1);
        Assert.NotNull(properties2);

        Assert.Equal(properties1, properties2);
    }
}