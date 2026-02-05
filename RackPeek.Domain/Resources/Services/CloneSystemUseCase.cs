using RackPeek.Domain.Helpers;

namespace RackPeek.Domain.Resources.Services;

public class CloneServiceUseCase(IServiceRepository repository, IResourceRepository resourceRepo) : IUseCase
{
    public async Task ExecuteAsync(string originalName, string cloneName)
    {
        originalName = Normalize.ServiceName(originalName);
        ThrowIfInvalid.ResourceName(originalName);
        
        cloneName = Normalize.ServiceName(cloneName);
        ThrowIfInvalid.ResourceName(cloneName);

        var existingResourceKind = await resourceRepo.GetResourceKindAsync(cloneName);
        if (!string.IsNullOrEmpty(existingResourceKind))
            throw new ConflictException($"{existingResourceKind} resource '{cloneName}' already exists.");
        
        var original = await repository.GetByNameAsync(originalName);
        if (original == null)
        {
            throw new NotFoundException($"Resource '{originalName}' not found.");
        }

        Network? clonedNetwork = null;
        if (original.Network != null)
        {
            clonedNetwork = new Network()
            {
                Ip = original.Network.Ip,
                Port = original.Network.Port,
                Protocol = original.Network.Protocol,
                Url = original.Network.Url,
            };
        }
        
        var clone = new Service()
        {
            Name = cloneName,
            Network = clonedNetwork,
            RunsOn = original.RunsOn,
        };
        
        await repository.AddAsync(clone);
    }
}