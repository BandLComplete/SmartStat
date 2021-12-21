using System.Text;
using Domain;

namespace Tests
{
	public class TestClient : Client
	{
		public async Task<bool> Login(string name, string password)
		{
			return await SendPost<bool>(ToDictionary(name, password), Api.Login);
		}

		public async Task<bool> Register(string name, string password)
		{
			return await SendPost<bool>(ToDictionary(name, password), Api.Register);
		}

		public async Task<bool> DeleteUser(string name, string password)
		{
			return await SendPost<bool>(ToDictionary(name, password), Api.DeleteUser);
		}

		private async Task<T> SendPost<T>(Dictionary<string, string> queries, string method)
		{
			var builder = new StringBuilder($"{Url}/{method}web");
			var isFirst = true;
			foreach (var query in queries)
			{
				builder.Append(isFirst ? "?" : "&");
				isFirst = false;
				builder.Append(query.Key);
				builder.Append("=");
				builder.Append(query.Value);
			}

			var response = await client.PostAsync(builder.ToString(), null);
			return await ReadBody<T>(response);
		}

		private static Dictionary<string, string> ToDictionary(string name, string password)
		{
			return new Dictionary<string, string>
			{
				["name"] = name,
				["password"] = password
			};
		}

		protected override string Url => "https://localhost:5001";
	}
}