namespace IPv4AndIPv6AnalyseTool;

internal class IPv6AnalyseTool
{
    public static bool IsIPv6(string? ip)
    {
        var parts = ip?.Split(':');
        if (parts?.Length != 8)
        {
            return false;
        }
        
        foreach (var part in parts)
        {
            if (part.Length > 4)
            {
                return false;
            }
        }
        
        return true;
    }
}