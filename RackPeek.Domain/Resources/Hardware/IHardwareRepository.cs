namespace RackPeek.Domain.Resources.Hardware;

public interface IHardwareRepository
{
    Task<IReadOnlyList<Models.Hardware>> GetAllAsync();
}