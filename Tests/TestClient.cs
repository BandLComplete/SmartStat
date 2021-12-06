using Domain;

namespace FirstApp.Service
{
	public class TestClient : Client
	{
		protected override string Url => "https://localhost:5001";
	}
}