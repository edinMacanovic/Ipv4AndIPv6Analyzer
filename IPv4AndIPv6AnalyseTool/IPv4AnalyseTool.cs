using System.Text;

namespace IPv4AndIPv6AnalyseTool;

internal class IPv4AnalyseTool
{
    public static bool IsIPv4(string? ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
        {
            return false;
        }

        var partsWithSuffix = ip.Split('/');
        if (partsWithSuffix.Length > 2)
        {
            return false;
        }

        var ipAddress = partsWithSuffix[0];
        var suffix = partsWithSuffix.Length == 2 ? partsWithSuffix[1] : null;

        if (suffix != null && Convert.ToInt32(suffix) < 0 || Convert.ToInt32(suffix) >= 32)
        {
            return false;
        }

        var parts = ipAddress.Split('.');
        if (parts.Length != 4)
        {
            return false;
        }

        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var num) || num < 0 || num > 255)
            {
                return false;
            }
        }

        return true;
    }

    public static void CalculateIPv4(string? ip)
    {
        if (string.IsNullOrEmpty(ip))
        {
            Console.WriteLine("Bitte eine gültige IP-Adresse eingeben.");
            return;
        }

        try
        {
            var parts = ip.Split('/');
            var ipAddress = parts[0];
            var classlessInterDomainRouting = int.Parse(parts[1]);

            var ipDecimal = ConvertIpToUint32(ipAddress);

            var subnetMask = GetSubnetMask(classlessInterDomainRouting);

            var netId = ipDecimal & subnetMask;

            var broadcast = netId | ~subnetMask;


            var firstHost = netId + 1;
            var lastHost = broadcast - 1;

            var hostCount = (int) (lastHost - firstHost + 1);

            Console.WriteLine($"Eingegebene IP: {ip}");
            Console.WriteLine($"Subnetzmaske: {ToDottedDecimal(subnetMask)}");
            Console.WriteLine($"Netzwerk-ID: {ToDottedDecimal(netId)}");
            Console.WriteLine($"Broadcast-Adresse: {ToDottedDecimal(broadcast)}");
            Console.WriteLine($"Host-Bereich: {ToDottedDecimal(firstHost)} - {ToDottedDecimal(lastHost)}");
            Console.WriteLine($"Anzahl der Hosts: {hostCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    private static uint GetSubnetMask(int cidr)
    {
        return cidr == 0 ? 0 : uint.MaxValue << (32 - cidr);
    }

    private static uint ConvertIpToUint32(string ip)
    {
        var parts = ip.Split('.');

        return (uint.Parse(parts[0]) << 24) |
               (uint.Parse(parts[1]) << 16) |
               (uint.Parse(parts[2]) << 8) |
               uint.Parse(parts[3]);
    }

    private static string ToDottedDecimal(uint value)
    {
        return $"{(value >> 24) & 255}.{(value >> 16) & 255}.{(value >> 8) & 255}.{value & 255}";
    }
}