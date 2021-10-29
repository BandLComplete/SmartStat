using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Database
{
	public class User
	{
		[Key]
		public string Name { get; set; }
		public string Password { get; set; }
	}
}