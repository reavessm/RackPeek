namespace RackPeek.Domain.Resources.SystemResources;

public interface ISystemRepository
{
    Task<IReadOnlyList<SystemResource>> GetAllAsync();
}