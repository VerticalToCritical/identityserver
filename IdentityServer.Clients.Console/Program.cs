using System;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServer.Clients.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }

        private static async Task MainTaskAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                System.Console.WriteLine(disco.Error);
                return;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "client","secret");
        }
    }
}
