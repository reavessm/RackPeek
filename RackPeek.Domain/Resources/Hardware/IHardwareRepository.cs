namespace RackPeek.Domain.Resources.Hardware;

public interface IHardwareRepository
{
    Task<IReadOnlyList<Models.Hardware>> GetAllAsync();
    Task AddAsync(Models.Hardware hardware);
    Task UpdateAsync(Models.Hardware hardware);
    Task DeleteAsync(string name);
    Task<Models.Hardware?> GetByNameAsync(string name);
}