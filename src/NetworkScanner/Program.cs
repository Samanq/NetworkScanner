using System.Net;
using System.Net.NetworkInformation;
using System.Text;


List<IPAddress> iPAddresses = new List<IPAddress>();

Console.Write("Enter the first IP: ");
var firstIp = Console.ReadLine();

Console.Write("Enter the last IP: ");
var lastIp = Console.ReadLine();

var network = firstIp.Substring(0, firstIp.LastIndexOf('.') + 1);

var startRange = int.Parse(firstIp.Substring(firstIp.LastIndexOf('.') + 1));
var lastRange = int.Parse(lastIp.Substring(lastIp.LastIndexOf('.') + 1));


for (int i = startRange; i <= lastRange; i++)
{
    iPAddresses.Add(IPAddress.Parse($"{network}{i}"));
}


Ping pingSender = new Ping();
PingOptions options = new PingOptions();

// Use the default Ttl value which is 128,
// but change the fragmentation behavior.
options.DontFragment = true;

// Create a buffer of 32 bytes of data to be transmitted.
string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
byte[] buffer = Encoding.ASCII.GetBytes(data);
int timeout = 120;


foreach (var ip in iPAddresses)
{
    PingReply reply = pingSender.Send(ip, timeout, buffer, options);
    if (reply.Status == IPStatus.Success)
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }
    Console.WriteLine($"{reply.Address} - {reply.Status}");
    Console.ResetColor();
}

