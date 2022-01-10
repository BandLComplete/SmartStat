namespace EntryPoint.Database;

public class PracticeDb
{
#pragma warning disable 8618

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Name { get; set; }
	public string Users { get; set; }
	public DateTime Date { get; set; }
	public TimeSpan Length { get; set; }
	public string Place { get; set; }
	public string Type { get; set; }
	public string Description { get; set; }
	public string? Tag { get; set; }
	public TimeSpan? Notification { get; set; }

#pragma warning restore 8618
}