using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Database
{
	public class User
	{
#pragma warning disable 8618

		[Key] public string Name { get; set; }
		public string Password { get; set; }

#pragma warning restore 8618
	}
}