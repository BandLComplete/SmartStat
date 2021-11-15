using System;
using EntryPoint.Database;

namespace EntryPoint.Models
{
	// public class Practice
	// {
	//     public int Id { get; }
	//     public string Name { get; set; }
	//     public string[] Users { get; set; }
	//     public DateTime Date { get; set; }
	//     public TimeSpan Length { get; set; }
	//     public string Place { get; set; }
	//     public string Type { get; set; }
	//     public string Description { get; set; }
	//     public string? Tag { get; set; }
	//     public TimeSpan? Notification { get; set; }
	//
	//     public Practice(PracticeDb practiceDb)
	//     {
	//         Id = practiceDb.Id;
	//         Name = practiceDb.Name;
	//         Users = practiceDb.Users.Split(separator);
	//         Date = practiceDb.Date;
	//         Length = practiceDb.Length;
	//         Place = practiceDb.Place;
	//         Type = practiceDb.Type;
	//         Description = practiceDb.Description;
	//         Tag = practiceDb.Tag;
	//         Notification = practiceDb.Notification;
	//     }
	//
	//     public PracticeDb ToDbModel()
	//     {
	//         return new PracticeDb
	//         {
	//             Id = Id,
	//             Name = Name,
	//             Users = string.Join(separator, Users),
	//             Date = Date,
	//             Length = Length,
	//             Place = Place,
	//             Type = Type,
	//             Description = Description,
	//             Tag = Tag,
	//             Notification = Notification
	//         };
	//     }
	//
	//     private const char separator = '\n';
	// }
}