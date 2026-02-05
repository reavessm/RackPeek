using RackPeek.Domain.Helpers;
using RackPeek.Domain.Resources.Models;
using RackPeek.Domain.Resources.Services;

namespace RackPeek.Domain.Resources.SystemResources.UseCases;

public class CloneSystemUseCase(ISystemRepository repository, IResourceRepository resourceRepo) : IUseCase
{
    public async Task ExecuteAsync(string originalName, string cloneName)
    {
        originalName = Normalize.SystemName(originalName);
        ThrowIfInvalid.ResourceName(originalName);
        
        cloneName = Normalize.SystemName(cloneName);
        ThrowIfInvalid.ResourceName(cloneName);

        var existingResourceKind = await resourceRepo.GetResourceKindAsync(cloneName);
        if (!string.IsNullOrEmpty(existingResourceKind))
            throw new ConflictException($"{existingResourceKind} resource '{cloneName}' already exists.");
        
        var original = await repository.GetByNameAsync(originalName);
        if (original == null)
        {
            throw new NotFoundException($"Resource '{originalName}' not found.");
        }

        List<Drive>? clonedDrives = null;
        if (original.Drives != null)
        {
            clonedDrives = original
                .Drives
                .Select(drive => new Drive() { Size = drive.Size, Type = drive.Type })
                .ToList();
        }
        
        var clone = new SystemResource()
        {
            Name = cloneName,
            Cores = original.Cores,
            Kind = original.Kind,
            Os = original.Os,
            Ram = original.Ram,
            Type = original.Type,
            Drives = clonedDrives,
            RunsOn = original.RunsOn,
        };
        
        await repository.AddAsync(clone);
    }
}