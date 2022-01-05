using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class DomainTests
    {
        private const string name = "TestName";
        private const string secondName = "Whoyakkka";

        private readonly TestClient client = new();

        private readonly Practice otherPractice = new()
        {
            Name = "TestName2",
            Date = DateTime.Now,
            Description = "Some text...",
            Length = TimeSpan.FromMinutes(80),
            Notification = null,
            Place = "Шарага",
            Tag = "A$$",
            Type = "Light",
            Users = new[] { name, secondName }
        };

        private readonly Stat otherStat = new()
        {
            User = name,
            Date = DateTime.Now.AddDays(-1),
            Name = "Прэс качат",
            Value = 15,
            Unit = "шт"
        };

        private readonly Practice practice = new()
        {
            Name = "TestName",
            Date = DateTime.Now.AddMinutes(14),
            Description = "Some text...",
            Length = TimeSpan.FromMinutes(90),
            Notification = TimeSpan.FromHours(1),
            Place = "Шарага",
            Tag = "A$$",
            Type = "Light",
            Users = new[] { name }
        };

        private readonly Stat stat = new()
        {
            User = name,
            Date = DateTime.Now,
            Name = "Прэс качат",
            Value = 20,
            Unit = "шт"
        };

        private readonly User user = new()
        {
            Name = name,
            Password = "ShortPassword"
        };

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
        public async Task UserTestWeb()
        {
            await client.DeleteUser(user.Name, user.Password);
            (await client.Login(user.Name, user.Password)).Should().BeFalse();
            (await client.Register(user.Name, user.Password)).Should().BeTrue();
            (await client.Login(user.Name, user.Password)).Should().BeTrue();
            (await client.DeleteUser(user.Name, user.Password)).Should().BeTrue();
            (await client.Login(user.Name, user.Password)).Should().BeFalse();
        }

        [Test]
        public async Task PracticeTest()
        {
            await client.DeletePractice(practice);
            await client.DeletePractice(otherPractice);
            (await client.GetPractices(new Practice { Date = DateTime.Now })).Should().BeEmpty();
            (await client.AddPractice(practice)).Should().BeTrue();
            (await client.AddPractice(otherPractice)).Should().BeTrue();
            (await client.GetPractices(new Practice { Date = DateTime.Now })).Should()
                .BeEquivalentTo(new[] { practice, otherPractice });
            (await client.DeletePractice(practice)).Should().BeTrue();
            (await client.DeletePractice(otherPractice)).Should().BeTrue();
            (await client.GetPractices(new Practice { Date = DateTime.Now.Date })).Should().BeEmpty();
        }

        [Test]
        public async Task StatTest()
        {
            var statToFind = new Stat { User = stat.User, Name = stat.Name };
            await client.PatchStat(stat, DbAction.Delete);
            await client.PatchStat(otherStat, DbAction.Delete);
            (await client.GetStats(statToFind)).Should().BeEmpty();
            ;
            (await client.PatchStat(stat, DbAction.Add)).Should().BeTrue();
            (await client.PatchStat(otherStat, DbAction.Add)).Should().BeTrue();
            (await client.GetStats(statToFind)).Should().BeEquivalentTo(new[] { stat, otherStat });
            (await client.GetStats(new Stat { User = stat.User, Date = stat.Date })).Should()
                .BeEquivalentTo(new[] { stat });
            stat.Value = 25;
            (await client.PatchStat(stat, DbAction.Update)).Should().BeTrue();
            (await client.GetStats(new Stat { User = stat.User, Date = stat.Date })).Should()
                .BeEquivalentTo(new[] { stat });
            (await client.PatchStat(stat, DbAction.Delete)).Should().BeTrue();
            (await client.PatchStat(otherStat, DbAction.Delete)).Should().BeTrue();
            (await client.GetStats(statToFind)).Should().BeEmpty();
        }
    }
}