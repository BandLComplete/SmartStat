namespace Tests;

public class TestClient : Client
{
	protected override string Url => "https://localhost:5001";

	public async Task<bool> Login(string name, string password)
	{
		return await SendPost<bool>(Api.Login, ToQueries(name, password));
	}

	public async Task<bool> Register(string name, string password)
	{
		return await SendPost<bool>(Api.Register, ToQueries(name, password));
	}

	public async Task<bool> DeleteUser(string name, string password)
	{
		return await SendPost<bool>(Api.DeleteUser, ToQueries(name, password));
	}

	private async Task<T> SendPost<T>(string method, params (string Name, string Value)[] queries)
	{
		var response = await client.PostAsync(AddQueries($"{Url}/{method}web", queries), null);
		return await ReadBody<T>(response);
	}

	private static (string Name, string Value)[] ToQueries(string name, string password)
	{
		return new[] { ("name", name), ("password", password) };
	}
}