using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.Crud;

using RackPeek.Domain.Resources.Hardware.Models;

public class UpdateServerUseCase(IHardwareRepository repository)
{
    public async Task ExecuteAsync(
        string name,
        int? ramGb = null,
        bool? ipmi = null,
        string? cpuModel = null,
        int? cores = null,
        int? threads = null
    )
    {
        var server = await repository.GetByNameAsync(name) as Server;
        if (server == null)
            throw new InvalidOperationException($"Server '{name}' not found.");

        // ---- RAM ----
        if (ramGb.HasValue)
        {
            server.Ram ??= new Ram();
            server.Ram.Size = ramGb.Value;
        }

        // ---- IPMI ----
        if (ipmi.HasValue)
        {
            server.Ipmi = ipmi.Value;
        }

        // ---- CPU (first CPU for now) ----
        if (cpuModel != null || cores.HasValue || threads.HasValue)
        {
            server.Cpus ??= new List<Cpu> { new Cpu() };

            var cpu = server.Cpus.First();

            if (cpuModel != null)
                cpu.Model = cpuModel;

            if (cores.HasValue)
                cpu.Cores = cores.Value;

            if (threads.HasValue)
                cpu.Threads = threads.Value;
        }

        await repository.UpdateAsync(server);
    }
}
