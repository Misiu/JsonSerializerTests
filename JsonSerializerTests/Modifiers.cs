using System.Diagnostics;
using JsonSerializerTests.Exception;

namespace JsonSerializerTests;

public static  class Modifiers
{
    public static void OnlyBaseExceptionProperties(System.Text.Json.Serialization.Metadata.JsonTypeInfo typeInfo)
    {
        Debug.WriteLine($"Type in System.Text.Json: {typeInfo.Type}");

        if (!typeInfo.Type.IsSubclassOf(typeof(BaseException)))
        {
            return;
        }

        foreach (var property in typeInfo.Properties)
        {
            if (property.Name.Equals(nameof(BaseException.Type), StringComparison.InvariantCultureIgnoreCase)
                || property.Name.Equals(nameof(BaseException.Details), StringComparison.InvariantCultureIgnoreCase)
                || property.Name.Equals(nameof(BaseException.Description), StringComparison.InvariantCultureIgnoreCase))
            {
                property.ShouldSerialize = static (_, _) => true;
            }
            else
            {
                property.ShouldSerialize = static (_, _) => false;
            }
        }
    }
}