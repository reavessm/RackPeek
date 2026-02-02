using RackPeek.Domain.Helpers;
using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.Routers;

public class DeleteRouterUseCase(IHardwareRepository repository) : IUseCase
{
    public async Task ExecuteAsync(string name)
    {
        ThrowIfInvalid.ResourceName(name);

        if (await repository.GetByNameAsync(name) is not Router hardware)
            throw new NotFoundException($"Router '{name}' not found.");

        await repository.DeleteAsync(name);
    }
}