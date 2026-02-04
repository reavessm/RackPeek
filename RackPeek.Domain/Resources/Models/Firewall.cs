namespace RackPeek.Domain.Resources.Hardware.Models;

public class Firewall : Hardware
{
    public const string KindLabel = "Firewall";
    public string? Model { get; set; }
    public bool? Managed { get; set; }
    public bool? Poe { get; set; }
    public List<Port>? Ports { get; set; }
}