namespace RackPeek.Domain.Resources.Hardware.Models;

public class Switch : Hardware
{
    public const string KindLabel = "Switch";
    public string? Model { get; set; }
    public bool? Managed { get; set; }
    public bool? Poe { get; set; }
    public List<Port>? Ports { get; set; }
}