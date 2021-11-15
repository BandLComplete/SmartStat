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

		public async Task<Practice[]> GetPractices()
		{
			var response = await client.GetAsync(url + "/GetPractices").ConfigureAwait(false);
			return JsonSerializer.Deserialize<Practice[]>(await response.Content.ReadAsStringAsync()
				.ConfigureAwait(false));
		}

		private const string url = "http://SmartStat.somee.com";

		private readonly HttpClient client;
	}
}