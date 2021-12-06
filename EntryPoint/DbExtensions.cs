using Domain;
using EntryPoint.Database;

namespace EntryPoint
{
	public static class DbExtensions
	{
		public static User ToModel(this UserDb userDb)
		{
			return new User
			{
				Name = userDb.Name,
				Password = userDb.Password,
			};
		}

		public static UserDb ToDb(this User user)
		{
			return new UserDb
			{
				Name = user.Name,
				Password = user.Password,
			};
		}

		public static Practice ToModel(this PracticeDb practiceDb)
		{
			return new Practice
			{
				Name = practiceDb.Name,
				Users = practiceDb.Users.Split(separator),
				Date = practiceDb.Date,
				LengthInMinutes = practiceDb.LengthInMinutes,
				Place = practiceDb.Place,
				Type = practiceDb.Type,
				Description = practiceDb.Description,
				Tag = practiceDb.Tag,
				Notification = practiceDb.Notification,
			};
		}

		public static PracticeDb ToDb(this Practice practice)
		{
			return new PracticeDb
			{
				Name = practice.Name,
				Users = string.Join(separator, practice.Users),
				Date = practice.Date,
				LengthInMinutes = practice.LengthInMinutes,
				Place = practice.Place,
				Type = practice.Type,
				Description = practice.Description,
				Tag = practice.Tag,
				Notification = practice.Notification
			};
		}

		private const char separator = '\n';
	}
}