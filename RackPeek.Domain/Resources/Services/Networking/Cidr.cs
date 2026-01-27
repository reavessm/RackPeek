using System;

namespace RackPeek.Domain.Resources.Services.Networking;


public readonly struct Cidr
{
    public uint Network { get; }
    public uint Mask { get; }
    public int Prefix { get; }

    public Cidr(uint network, uint mask, int prefix)
    {
        Network = network;
        Mask = mask;
        Prefix = prefix;
    }

    public bool Contains(uint ip) => (ip & Mask) == Network;

    public override string ToString()
    {
        return $"{IpHelper.ToIp(Network)}/{Prefix}";
    }

    public static Cidr Parse(string cidr)
    {
        var parts = cidr.Split('/');
        if (parts.Length != 2)
            throw new ArgumentException($"CIDR must be in format a.b.c.d/nn: {cidr}");

        uint ip = IpHelper.ToUInt32(parts[0]);
        int prefix = int.Parse(parts[1]);

        uint mask = IpHelper.MaskFromPrefix(prefix);
        uint network = ip & mask;

        return new Cidr(network, mask, prefix);
    }
}