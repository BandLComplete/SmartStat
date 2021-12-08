using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain
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

		public async Task<bool> Login(User user)
		{
			return await SendPost<User, bool>(user, Api.Login);
		}

		public async Task<bool> Register(User user)
		{
			return await SendPost<User, bool>(user, Api.Register);
		}

		public async Task<bool> DeleteUser(User user)
		{
			return await SendPost<User, bool>(user, Api.DeleteUser);
		}

		public async Task<bool> AddPractice(Practice practice)
		{
			return await SendPost<Practice, bool>(practice, Api.AddPractice);
		}

		public async Task<bool> DeletePractice(Practice practice)
		{
			return await SendPost<Practice, bool>(practice, Api.DeletePractice);
		}

		/// <summary>
		/// Поиск тренировок
		/// </summary>
		/// <param name="practice">Сюда пихать нужные значения для поиска</param>
		/// <returns></returns>
		public async Task<Practice[]> GetPractices(Practice practice)
		{
			return await SendPost<Practice, Practice[]>(practice, Api.GetPractices);
		}

		private async Task<TResult> SendPost<T, TResult>(T body, string method)
		{
			var json = JsonSerializer.Serialize(body);
			var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
			var response = await client.PostAsync($"{Url}/{method}", content).ConfigureAwait(false);
			if (!response.IsSuccessStatusCode)
				throw new Exception(response.ToString());

			var ms = new MemoryStream();
			await response.Content.CopyToAsync(ms);
			var s = Encoding.UTF8.GetString(ms.ToArray());
			return JsonSerializer.Deserialize<TResult>(s) ??
			       throw new NullReferenceException($"Failed to deserialized {s}");
		}

		protected virtual string Url => "http://SmartStat.somee.com";

		private readonly HttpClient client;
	}
}