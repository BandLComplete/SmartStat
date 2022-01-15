using System;

namespace Domain
{
	public static class Extensions
	{
		public static DateTime SetUtcKind(this DateTime date, bool withTime = false) =>
			new DateTime(date.Year, date.Month, date.Day, withTime ? date.Hour : 0, withTime ? date.Minute : 0, 0,
				DateTimeKind.Utc);
	}
}