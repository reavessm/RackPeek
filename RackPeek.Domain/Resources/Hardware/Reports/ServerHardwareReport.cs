using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.Reports;

public record ServerHardwareReport(
    IReadOnlyList<ServerHardwareRow> Servers
);

public record ServerHardwareRow(
    string Name,
    string CpuSummary,
    int TotalCores,
    int TotalThreads,
    int RamGb,
    int TotalStorageGb,
    int SsdStorageGb,
    int HddStorageGb,
    int TotalNicPorts,
    int MaxNicSpeedGb,
    bool Ipmi
);

public class ServerHardwareReportUseCase(IHardwareRepository repository)
{
    public async Task<ServerHardwareReport> ExecuteAsync()
    {
        var hardware = await repository.GetAllAsync();
        var servers = hardware.OfType<Server>();

        var rows = servers.Select(server =>
        {
            var totalCores = server.Cpus?.Sum(c => c.Cores) ?? 0;
            var totalThreads = server.Cpus?.Sum(c => c.Threads) ?? 0;

            var cpuSummary = server.Cpus == null
                ? "Unknown"
                : string.Join(", ",
                    server.Cpus
                        .GroupBy(c => c.Model)
                        .Select(g => $"{g.Count()}× {g.Key}"));

            var ramGb = server.Ram?.Size ?? 0;

            var totalStorage = server.Drives?.Sum(d => d.Size) ?? 0;
            var ssdStorage = server.Drives?
                .Where(d => d.Type == "ssd")
                .Sum(d => d.Size) ?? 0;
            var hddStorage = server.Drives?
                .Where(d => d.Type == "hdd")
                .Sum(d => d.Size) ?? 0;

            var totalNicPorts = server.Nics?.Sum(n => n.Ports) ?? 0;
            var maxNicSpeed = server.Nics?.Max(n => n.Speed) ?? 0;

            return new ServerHardwareRow(
                Name: server.Name,
                CpuSummary: cpuSummary,
                TotalCores: totalCores,
                TotalThreads: totalThreads,
                RamGb: ramGb,
                TotalStorageGb: totalStorage,
                SsdStorageGb: ssdStorage,
                HddStorageGb: hddStorage,
                TotalNicPorts: totalNicPorts,
                MaxNicSpeedGb: maxNicSpeed,
                Ipmi: server.Ipmi ?? false
            );
        }).ToList();

        return new ServerHardwareReport(rows);
    }
}
