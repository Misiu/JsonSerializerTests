using System.Diagnostics;
using JsonSerializerTests.Exception;

namespace JsonSerializerTests;

public sealed class BaseExceptionContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
{
    public BaseExceptionContractResolver()
    {
        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy();
    }

    protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
    {
        Debug.WriteLine($"Type in Newtonsoft.Json: {type}");
        return !type.IsSubclassOf(typeof(BaseException)) ? base.CreateProperties(type, memberSerialization) : base.CreateProperties(typeof(BaseException), memberSerialization).Where(IsExceptionProperty).ToList();
    }

    private bool IsExceptionProperty(Newtonsoft.Json.Serialization.JsonProperty property)
    {
        if (property.PropertyName == null)
        {
            return false;
        }

        return property.PropertyName.Equals(nameof(BaseException.Type), StringComparison.InvariantCultureIgnoreCase)
               || property.PropertyName.Equals(nameof(BaseException.Details), StringComparison.InvariantCultureIgnoreCase)
               || property.PropertyName.Equals(nameof(BaseException.Description), StringComparison.InvariantCultureIgnoreCase);
    }
}