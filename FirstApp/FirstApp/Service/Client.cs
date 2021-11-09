using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstApp.Service
{
    public class Client
    {
        public Client()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            client = new HttpClient(clientHandler);
        }
        
        public async Task<bool> AddPractice(Practice practice)
        {
            var s = JsonSerializer.Serialize(practice);
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(s));
            var response = await client.PostAsync(url + "/AddPractice", content).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        private const string url = "https://localhost:44332";
        
        private readonly HttpClient client;
    }
}