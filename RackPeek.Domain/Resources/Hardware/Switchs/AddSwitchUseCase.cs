namespace RackPeek.Domain.Resources.Hardware.Switchs;

public class AddSwitchUseCase(IHardwareRepository repository)
{
    public async Task ExecuteAsync(string name)
    {
        // basic guard rails
        var existing = await repository.GetByNameAsync(name);
        if (existing != null)
            throw new InvalidOperationException($"Switch '{name}' already exists.");

        var switchResource = new Models.Switch
        {
            Name = name,
        };

        await repository.AddAsync(switchResource);
    }
}