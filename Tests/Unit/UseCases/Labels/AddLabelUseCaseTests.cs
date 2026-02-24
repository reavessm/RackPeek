using NSubstitute;
using RackPeek.Domain.Helpers;
using RackPeek.Domain.Persistence;
using RackPeek.Domain.Resources;
using RackPeek.Domain.Resources.Servers;
using RackPeek.Domain.UseCases.Labels;

namespace Tests.Unit.UseCases.Labels;

public class AddLabelUseCaseTests
{
    private readonly IResourceCollection _repo = Substitute.For<IResourceCollection>();

    [Fact]
    public async Task execute_async__new_label__sets_key_value_and_updates()
    {
        // Arrange
        var server = new Server { Name = "db-01", Labels = new() };
        _repo.GetByNameAsync("db-01").Returns(server);
        var sut = new AddLabelUseCase<Server>(_repo);

        // Act
        await sut.ExecuteAsync("db-01", "env", "production");

        // Assert
        Assert.Equal("production", server.Labels["env"]);
        await _repo.Received(1).UpdateAsync(server);
    }

    [Fact]
    public async Task execute_async__existing_key__overwrites_value()
    {
        // Arrange
        var server = new Server
        {
            Name = "db-01",
            Labels = new() { ["env"] = "staging" }
        };
        _repo.GetByNameAsync("db-01").Returns(server);
        var sut = new AddLabelUseCase<Server>(_repo);

        // Act
        await sut.ExecuteAsync("db-01", "env", "production");

        // Assert
        Assert.Equal("production", server.Labels["env"]);
    }

    [Fact]
    public async Task execute_async__nonexistent_resource__throws_not_found()
    {
        // Arrange
        _repo.GetByNameAsync("ghost").Returns((Resource?)null);
        var sut = new AddLabelUseCase<Server>(_repo);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(
            () => sut.ExecuteAsync("ghost", "env", "production"));
        Assert.Contains("ghost", ex.Message);
        await _repo.DidNotReceive().UpdateAsync(Arg.Any<Resource>());
    }
}
