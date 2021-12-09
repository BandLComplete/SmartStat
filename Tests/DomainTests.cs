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
			(await client.Login(user)).Should().BeFalse();
			(await client.Register(user)).Should().BeTrue();
			(await client.Login(user)).Should().BeTrue();
			(await client.DeleteUser(user)).Should().BeTrue();
			(await client.Login(user)).Should().BeFalse();
		}

		[Test]
		public async Task PracticeTest()
		{
			await client.DeletePractice(practice);
			await client.DeletePractice(otherPractice);
			(await client.GetPractices(new Practice { Date = new DateTime(2021, 5, 16) })).Should().BeEmpty();
			(await client.AddPractice(practice)).Should().BeTrue();
			(await client.AddPractice(otherPractice)).Should().BeTrue();
			(await client.GetPractices(new Practice { Date = new DateTime(2021, 5, 16) })).Should()
				.BeEquivalentTo(new[] { practice, otherPractice });
			(await client.DeletePractice(practice)).Should().BeTrue();
			(await client.DeletePractice(otherPractice)).Should().BeTrue();
			(await client.GetPractices(new Practice { Date = new DateTime(2021, 5, 16) })).Should().BeEmpty();
		}

		private const string name = "TestName";
		private const string secondName = "Whoyakkka";

		private readonly TestClient client = new();

		private readonly User user = new()
		{
			Name = name,
			Password = "ShortPassword",
		};

		private readonly Practice practice = new()
		{
			Name = "TestName",
			Date = new DateTime(2021, 5, 16, 1, 2, 3),
			Description = "Some text...",
			LengthInMinutes = 90,
			Notification = null,
			Place = "Шарага",
			Tag = "A$$",
			Type = "Light",
			Users = new[] { name },
		};

		private readonly Practice otherPractice = new()
		{
			Name = "TestName2",
			Date = new DateTime(2021, 5, 16, 20, 50, 0),
			Description = "Some text...",
			LengthInMinutes = 90,
			Notification = null,
			Place = "Шарага",
			Tag = "A$$",
			Type = "Light",
			Users = new[] { name, secondName },
		};
	}
}