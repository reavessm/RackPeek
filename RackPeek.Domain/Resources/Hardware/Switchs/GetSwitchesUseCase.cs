namespace RackPeek.Domain.Resources.Hardware.Switchs;

public class GetSwitchesUseCase(IHardwareRepository repository)
{
    public async Task<IReadOnlyList<Models.Switch>> ExecuteAsync()
    {
        var hardware = await repository.GetAllAsync();
        return hardware.OfType<Models.Switch>().ToList();
    }
}