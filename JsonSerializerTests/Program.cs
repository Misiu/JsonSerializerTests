using System.Text.Json.Serialization.Metadata;
using JsonSerializerTests.Exception;

namespace JsonSerializerTests;

internal class Program
{
    static void Main(string[] args)
    {


        var baseExceptionContractResolver = new BaseExceptionContractResolver();

        var newtonsoftSettings = new Newtonsoft.Json.JsonSerializerSettings
            { Formatting = Newtonsoft.Json.Formatting.Indented, ContractResolver = baseExceptionContractResolver };

        var textJsonSettings = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { Modifiers.OnlyBaseExceptionProperties }
            }
        };

        System.Exception exception1 = new ValidationException("Test");
        var exception2 = new ValidationException("Test");

        var s1e1 = Newtonsoft.Json.JsonConvert.SerializeObject(exception1, newtonsoftSettings);
        var s2e1 = System.Text.Json.JsonSerializer.Serialize(exception1, textJsonSettings);

        Console.WriteLine(s1e1 == s2e1);

        var s1e2 = Newtonsoft.Json.JsonConvert.SerializeObject(exception2, newtonsoftSettings);
        var s2e2 = System.Text.Json.JsonSerializer.Serialize(exception2, textJsonSettings);

        Console.WriteLine(s1e2 == s2e2);

            
        Console.ReadLine();
    }
}