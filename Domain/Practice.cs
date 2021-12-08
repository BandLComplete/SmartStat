using System;

namespace Domain
{
	public class Practice
	{
#pragma warning disable 8618

		public string Name { get; set; }
		public string[] Users { get; set; }
		public DateTime Date { get; set; }
		public int LengthInMinutes { get; set; }
		public string Place { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string Tag { get; set; }
		public TimeSpan? Notification { get; set; } //todo: Сделать чтобы хотя бы в приложке вылетало

#pragma warning restore 8618

		public TimeSpan Length => TimeSpan.FromMinutes(LengthInMinutes);
	}
}