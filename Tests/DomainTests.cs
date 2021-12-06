using System;
using System.Threading.Tasks;
using Domain;
using FirstApp.Service;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
	public class DomainTests
	{
		[Test]
		public async Task UserTest()
		{
			await client.DeleteUser(user);

			(await client.Register(user)).Should().BeTrue();

			(await client.Login(user)).Should().BeTrue();
		}

		[Test]
		public async Task AddPracticeTest()
		{
			var result = await client.AddPractice(practice);
			result.Should().BeTrue();
		}

		private readonly TestClient client = new();

		private readonly User user = new()
		{
			Name = "TestName",
			Password = "ShortPassword",
		};

		private readonly Practice practice = new()
		{
			Name = "TestName",
			Date = new DateTime(2021, 11, 16, 1, 2, 3),
			Description = "Some text...",
			LengthInMinutes = 90,
			Notification = null,
			Place = "Шарага",
			Tag = "A$$",
			Type = "Light",
			Users = new[] { "TestUser" },
		};
	}
}