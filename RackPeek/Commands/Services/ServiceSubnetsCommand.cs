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
    
    private static string BuildUtilizationBar(double fullness, int width = 30)
    {
        fullness = Math.Clamp(fullness, 0, 100);
        int filled = (int)(width * (fullness / 100.0));
        int empty = width - filled;

        var color = fullness switch
        {
            < 50 => Color.Green,
            < 80 => Color.Yellow,
            _ => Color.Red
        };

        string filledBar = new string('█', filled);
        string emptyBar = new string('░', empty);

        return $"[{color.ToString().ToLower()}]{filledBar}[/]{emptyBar} {fullness:0}%";
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

       
        if (settings.Cidr is not null)
        {
            var services = result.Services;

            if (services.Count == 0)
            {
                AnsiConsole.MarkupLine($"[yellow]No services found in {settings.Cidr}[/]");
                return 0;
            }

            var table = new Table()
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

   
        var subnets = result.Subnets;
        
        subnets = subnets
            .OrderByDescending(s =>
            {
                var parts = s.Cidr.Split('/');
                int prefix = int.Parse(parts[1]);
                double alloc = Math.Pow(2, 32 - prefix) - 2;
                return alloc <= 0 ? 0 : (s.Count / alloc);
            })
            .ToList();
        
        if (subnets.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No subnets found.[/]");
            return 0;
        }

        var subnetTable = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("Subnet")
            .AddColumn("Services")
            .AddColumn("Utilization");

        foreach (var subnet in subnets)
        {
            var parts = subnet.Cidr.Split('/');
            int prefix = int.Parse(parts[1]);

            // allocatable addresses
            double alloc = Math.Pow(2, 32 - prefix) - 2;
            double used = subnet.Count;
            double fullness = alloc <= 0 ? 0 : (used / alloc) * 100;

            string bar = BuildUtilizationBar(fullness);

            subnetTable.AddRow(subnet.Cidr, subnet.Count.ToString(), bar);
        }

        AnsiConsole.Write(subnetTable);

        return 0;
    }
}
