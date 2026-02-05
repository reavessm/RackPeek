namespace RackPeek.Domain.Resources.Models;

public class Router : Hardware
{
    public const string KindLabel = "Router";
    public string? Model { get; set; }
    public bool? Managed { get; set; }
    public bool? Poe { get; set; }
    public List<Port>? Ports { get; set; }
}