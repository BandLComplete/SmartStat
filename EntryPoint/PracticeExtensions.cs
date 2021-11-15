using System;
using EntryPoint.Database;
using FirstApp.Service;

namespace EntryPoint
{
	public static class PracticeExtensions
	{
		public static Practice ToModel(this PracticeDb practiceDb)
		{
			return new Practice
			{
				Id = practiceDb.Id,
				Name = practiceDb.Name,
				Users = practiceDb.Users.Split(separator),
				Date = practiceDb.Date,
				Length = practiceDb.Length,
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
				Id = practice.Id,
				Name = practice.Name,
				Users = string.Join(separator, practice.Users),
				Date = practice.Date,
				Length = practice.Length,
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