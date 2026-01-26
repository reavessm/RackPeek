using RackPeek.Domain.Resources.Hardware.Models;

namespace RackPeek.Domain.Resources.Hardware.UpsUnits;

public class GetUpsUseCase(IHardwareRepository repository)
{
    public async Task<IReadOnlyList<Ups>> ExecuteAsync()
    {
        var hardware = await repository.GetAllAsync();
        return hardware.OfType<Ups>().ToList();
    }
}