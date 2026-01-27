using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RackPeek.Domain.Resources.Services.UseCases;
using Spectre.Console;
using Spectre.Console.Cli;

namespace RackPeek.Commands.Services;

public class ServiceSubnetsCommand(
    ILogger<ServiceSubnetsCommand> logger,
    IServiceProvider serviceProvider
) : AsyncCommand<ServiceSubnetsCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("--cidr <CIDR>")]
        public string? Cidr { get; set; }

        [CommandOption("--prefix <PREFIX>")]
        public int? Prefix { get; set; }
    }

    public override async Task<int> ExecuteAsync(
        CommandContext context,
        Settings settings,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<ServiceSubnetsUseCase>();

        var result = await useCase.ExecuteAsync(settings.Cidr, settings.Prefix, cancellationToken);

        // Handle invalid CIDR
        if (result.IsInvalidCidr)
        {
            AnsiConsole.MarkupLine($"[red]Invalid CIDR:[/] {result.InvalidCidrValue}");
            return 1;
        }

        // CIDR filter mode
        if (settings.Cidr is not null)
        {
            var services = result.Services;

            if (services.Count == 0)
            {
                AnsiConsole.MarkupLine($"[yellow]No services found in {settings.Cidr}[/]");
                return 0;
            }

            var table = new Table()
                .Expand()
                .Border(TableBorder.Rounded)
                .AddColumn("Name")
                .AddColumn("IP")
                .AddColumn("Runs On");

            foreach (var s in services)
                table.AddRow(s.Name, s.Ip, s.RunsOn ?? "Unknown");

            AnsiConsole.MarkupLine($"[green]Services in {result.FilteredCidr}[/]");
            AnsiConsole.Write(table);
            return 0;
        }

        // Subnet discovery mode
        var subnets = result.Subnets;

        if (subnets.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No subnets found.[/]");
            return 0;
        }

        var subnetTable = new Table()
            .Expand()
            .Border(TableBorder.Rounded)
            .AddColumn("Subnet")
            .AddColumn("Services");

        foreach (var subnet in subnets)
            subnetTable.AddRow(subnet.Cidr, subnet.Count.ToString());

        AnsiConsole.Write(subnetTable);
        return 0;
    }
}
