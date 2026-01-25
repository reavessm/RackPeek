using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace RackPeek.Resources;

public abstract class Resource
{
    [JsonPropertyName("kind")]
    [YamlMember(Alias = "kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    [YamlMember(Alias = "name")]
    public required string Name { get; set; }

    [JsonPropertyName("tags")]
    [YamlMember(Alias = "tags")]
    public Dictionary<string, string>? Tags { get; set; }
}