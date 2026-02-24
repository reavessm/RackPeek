using Microsoft.Extensions.DependencyInjection;
using RackPeek.Domain.Resources.Servers;
using RackPeek.Domain.UseCases;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shared.Rcl.Commands.Servers;

public class ServerDescribeCommand(
    IServiceProvider serviceProvider
) : AsyncCommand<ServerNameSettings>
{
    public override async Task<int> ExecuteAsync(
        CommandContext context,
        ServerNameSettings settings,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<IGetResourceByNameUseCase<Server>>();

        var server = await useCase.ExecuteAsync(settings.Name);

        var grid = new Grid()
            .AddColumn()
            .AddColumn();

        grid.AddRow("Name", server.Name);
        grid.AddRow("IPMI", server.Ipmi == true ? "yes" : "no");
        grid.AddRow("RAM", $"{server.Ram?.Size ?? 0} GB");

        if (server.Cpus != null)
            foreach (var cpu in server.Cpus)
                grid.AddRow("CPU", $"{cpu.Model} ({cpu.Cores}/{cpu.Threads})");

        if (server.Labels.Count > 0)
            grid.AddRow("Labels", string.Join(", ", server.Labels.Select(kvp => $"{kvp.Key}: {kvp.Value}")));

        AnsiConsole.Write(
            new Panel(grid)
                .Header("Server")
                .Border(BoxBorder.Rounded));

        return 0;
    }
}