namespace EntryPoint.Database;

public class StatDb
{
#pragma warning disable 8618

	[Key] public string UserDateName { get; set; }

	public string User { get; set; }
	public DateOnly Date { get; set; }
	public string Name { get; set; }
	public int Value { get; set; }
	public string? Unit { get; set; }

#pragma warning restore 8618
}