using RackPeek.Domain.Helpers;
using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.Firewalls;

public class DeleteFirewallUseCase(IHardwareRepository repository) : IUseCase
{
    public async Task ExecuteAsync(string name)
    {
        ThrowIfInvalid.ResourceName(name);

        if (await repository.GetByNameAsync(name) is not Firewall hardware)
            throw new NotFoundException($"Firewall '{name}' not found.");

        await repository.DeleteAsync(name);
    }
}