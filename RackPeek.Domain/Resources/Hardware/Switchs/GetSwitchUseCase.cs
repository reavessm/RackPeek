namespace RackPeek.Domain.Resources.Hardware.Switchs;

public class GetSwitchUseCase(IHardwareRepository repository)
{
    public async Task<Models.Switch?> ExecuteAsync(string name)
    {
        var hardware = await repository.GetByNameAsync(name);
        return hardware as Models.Switch;
    }
}