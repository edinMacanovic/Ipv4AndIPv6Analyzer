namespace IPv4AndIPv6AnalyseTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                Console.WriteLine("Bitte geben Sie eine IP-Adresse ein:");
                var ip = Console.ReadLine();
                
                if (IPv4AnalyseTool.IsIPv4(ip))
                {
                    Console.WriteLine("Die eingegebene IP-Adresse ist eine IPv4-Adresse.");
                    Console.WriteLine("Wollen Sie diese IP Adresse bearbeiten? (Ja/Nein)");
                    var answer = Console.ReadLine();
                    if (answer == "Ja")
                    {
                        IPv4AnalyseTool.CalculateIPv4(ip);
                    }
                    else
                    {
                        break;
                    }
                }
                else if (IPv6AnalyseTool.IsIPv6(ip))
                {
                    Console.WriteLine("Die eingegebene IP-Adresse ist eine IPv6-Adresse.");
                }
                else
                {
                    Console.WriteLine(
                        "Die eingegebene IP-Adresse ist keine gültige IPv4/6-Adresse. Bitte geben Sie eine gültige IPv4/6-Adresse ein:");
                }
            }
            
            
        }
    }
}