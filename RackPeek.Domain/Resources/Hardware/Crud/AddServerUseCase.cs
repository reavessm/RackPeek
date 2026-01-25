using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.Crud;

public class AddServerUseCase(IHardwareRepository repository)
{
    public async Task ExecuteAsync(
        string name,
        string? cpuModel,
        int? cores,
        int? threads,
        int? ramGb,
        bool? ipmi
    )
    {
        // basic guard rails
        var existing = await repository.GetByNameAsync(name);
        if (existing != null)
            throw new InvalidOperationException($"Server '{name}' already exists.");

        var server = new Server
        {
            Name = name,
            Cpus = [new Cpu()
            {
                Model = cpuModel,
                Cores = cores,
                Threads = threads
            }],
            Ram = new Ram
            {
                Size = ramGb
            },
            Ipmi = ipmi
        };

        await repository.AddAsync(server);
    }
}
