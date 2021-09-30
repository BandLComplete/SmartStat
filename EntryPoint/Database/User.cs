using System.ComponentModel.DataAnnotations.Schema;

namespace EntryPoint.Database
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}