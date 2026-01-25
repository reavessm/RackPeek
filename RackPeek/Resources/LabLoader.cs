using RackPeek.Resources.Hardware;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RackPeek.Resources;

public static class LabLoader
{
    public static List<Resource> Load(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithCaseInsensitivePropertyMatching()
            .WithTypeConverter(new StorageSizeYamlConverter())
            .Build();

        var raw = deserializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(yaml);

        var resources = new List<Resource>();

        foreach (var item in raw["resources"])
        {
            var kind = item["kind"].ToString();

            var typedYaml = new SerializerBuilder().Build().Serialize(item);
    
            Resource resource = kind switch
            {
                // Hardware
                "Server" => deserializer.Deserialize<Server>(typedYaml),
                "Switch" => deserializer.Deserialize<Switch>(typedYaml),
                "Firewall" => deserializer.Deserialize<Firewall>(typedYaml),
                "Router" => deserializer.Deserialize<Router>(typedYaml),
                "Desktop" => deserializer.Deserialize<Desktop>(typedYaml),
                "Laptop" => deserializer.Deserialize<Laptop>(typedYaml),
                "AccessPoint" => deserializer.Deserialize<AccessPoint>(typedYaml),
                "Ups" => deserializer.Deserialize<Ups>(typedYaml),
                
                // System
                "System" => deserializer.Deserialize<SystemResource>(typedYaml),
                _ => throw new InvalidOperationException($"Unknown kind: {kind}")
            };

            resources.Add(resource);
        }

        return resources;
    }
}