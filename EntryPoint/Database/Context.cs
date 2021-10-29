using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Database
{
	public class Context : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Practice> Practices { get; set; }
		
		public string DbPath { get; private set; }

		public Context()
		{
			var folder = Environment.CurrentDirectory;
			DbPath = Path.Combine(folder, "smartStat.db");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}
}