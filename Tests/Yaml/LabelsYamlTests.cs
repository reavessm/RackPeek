using RackPeek.Domain.Persistence;
using RackPeek.Domain.Persistence.Yaml;
using RackPeek.Domain.Resources.Servers;

namespace Tests.Yaml;

/// <summary>
/// Tests YAML serialization and deserialization of resource labels.
/// </summary>
public class LabelsYamlTests
{
    private static async Task<IResourceCollection> CreateSut(string yaml)
    {
        var tempDir = Path.Combine(
            Path.GetTempPath(),
            "RackPeekTests",
            Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(tempDir);

        var filePath = Path.Combine(tempDir, "config.yaml");
        await File.WriteAllTextAsync(filePath, yaml);

        var yamlResourceCollection =
            new YamlResourceCollection(filePath, new PhysicalTextFileStore(), new ResourceCollection());
        await yamlResourceCollection.LoadAsync();
        return yamlResourceCollection;
    }

    [Fact]
    public async Task deserialize_yaml_with_labels__resource_has_labels()
    {
        // Given
        var yaml = @"
resources:
  - kind: Server
    name: web-01
    labels:
      env: production
      owner: team-a
";

        var sut = await CreateSut(yaml);

        // When
        var server = await sut.GetByNameAsync<Server>("web-01");

        // Then
        Assert.NotNull(server);
        Assert.Equal(2, server.Labels.Count);
        Assert.Equal("production", server.Labels["env"]);
        Assert.Equal("team-a", server.Labels["owner"]);
    }

    [Fact]
    public async Task deserialize_yaml_without_labels__resource_has_empty_labels()
    {
        // Given - legacy YAML without labels section
        var yaml = @"
resources:
  - kind: Server
    name: web-01
";

        var sut = await CreateSut(yaml);

        // When
        var server = await sut.GetByNameAsync<Server>("web-01");

        // Then
        Assert.NotNull(server);
        Assert.NotNull(server.Labels);
        Assert.Empty(server.Labels);
    }

    [Fact]
    public async Task round_trip_labels__persisted_and_loaded()
    {
        // Given - add server with labels via collection, save, reload
        var tempDir = Path.Combine(
            Path.GetTempPath(),
            "RackPeekTests",
            Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var filePath = Path.Combine(tempDir, "config.yaml");
        await File.WriteAllTextAsync(filePath, "");

        var collection = new ResourceCollection();
        var yamlCollection = new YamlResourceCollection(filePath, new PhysicalTextFileStore(), collection);
        await yamlCollection.LoadAsync();

        var server = new Server
        {
            Name = "web-01",
            Labels = new Dictionary<string, string> { ["env"] = "production", ["owner"] = "team-a" }
        };
        await yamlCollection.AddAsync(server);

        // When - reload from file
        var reloaded = new YamlResourceCollection(filePath, new PhysicalTextFileStore(), new ResourceCollection());
        await reloaded.LoadAsync();
        var loaded = await reloaded.GetByNameAsync<Server>("web-01");

        // Then
        Assert.NotNull(loaded);
        Assert.Equal(2, loaded.Labels.Count);
        Assert.Equal("production", loaded.Labels["env"]);
        Assert.Equal("team-a", loaded.Labels["owner"]);
    }
}
