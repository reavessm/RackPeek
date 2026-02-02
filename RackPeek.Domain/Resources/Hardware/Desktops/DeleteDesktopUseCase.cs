using RackPeek.Domain.Helpers;

namespace RackPeek.Domain.Resources.Hardware.Desktops;

public class DeleteDesktopUseCase(IHardwareRepository repository) : IUseCase
{
    public async Task ExecuteAsync(string name)
    {
        ThrowIfInvalid.ResourceName(name);

        var hardware = await repository.GetByNameAsync(name);
        if (hardware == null)
            throw new NotFoundException($"Desktop '{name}' not found.");

        await repository.DeleteAsync(name);
    }
}