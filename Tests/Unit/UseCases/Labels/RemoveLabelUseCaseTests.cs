using NSubstitute;
using RackPeek.Domain.Persistence;
using RackPeek.Domain.Resources;
using RackPeek.Domain.Resources.Servers;
using RackPeek.Domain.UseCases.Labels;

namespace Tests.Unit.UseCases.Labels;

public class RemoveLabelUseCaseTests
{
    private readonly IResourceCollection _repo = Substitute.For<IResourceCollection>();

    [Fact]
    public async Task execute_async__existing_label__removes_key_and_updates()
    {
        // Arrange
        var server = new Server
        {
            Name = "db-01",
            Labels = new() { ["env"] = "production" }
        };
        _repo.GetByNameAsync("db-01").Returns(server);
        var sut = new RemoveLabelUseCase<Server>(_repo);

        // Act
        await sut.ExecuteAsync("db-01", "env");

        // Assert
        Assert.False(server.Labels.ContainsKey("env"));
        await _repo.Received(1).UpdateAsync(server);
    }

    [Fact]
    public async Task execute_async__key_not_present__does_not_update_repo()
    {
        // Arrange
        var server = new Server
        {
            Name = "db-01",
            Labels = new() { ["env"] = "production" }
        };
        _repo.GetByNameAsync("db-01").Returns(server);
        var sut = new RemoveLabelUseCase<Server>(_repo);

        // Act
        await sut.ExecuteAsync("db-01", "nonexistent");

        // Assert
        await _repo.DidNotReceive().UpdateAsync(Arg.Any<Resource>());
    }
}
