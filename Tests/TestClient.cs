namespace Tests;

public class TestClient : Client
{
	protected override string Url => "https://localhost:5001";

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
		var response = await client.PostAsync(AddQueries($"{Url}/{method}web", queries), null);
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
}