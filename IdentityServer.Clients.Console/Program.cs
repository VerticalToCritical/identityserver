using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace IdentityServer.Clients.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainTaskAsync().GetAwaiter().GetResult();
            System.Console.ReadLine();
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
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                System.Console.WriteLine(tokenResponse.Error);
                return;
            }

            System.Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/Values");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine(JArray.Parse(content));
            }
            else
            {
                System.Console.WriteLine(response.StatusCode);
            }            
        }
    }
}
