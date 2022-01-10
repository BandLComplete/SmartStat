namespace EntryPoint.Database;

public class UserDb
{
#pragma warning disable 8618

	[Key] public string Name { get; set; }

	public string Password { get; set; }

#pragma warning restore 8618
}