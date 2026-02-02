using RackPeek.Domain.Helpers;
using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.AccessPoints;

public class DeleteAccessPointUseCase(IHardwareRepository repository) : IUseCase
{
    public async Task ExecuteAsync(string name)
    {
        ThrowIfInvalid.ResourceName(name);

        if (await repository.GetByNameAsync(name) is not AccessPoint ap)
            throw new NotFoundException($"Access point '{name}' not found.");

        await repository.DeleteAsync(name);
    }
}