using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint.Database
{
    public class Context : DbContext
    {
        private readonly string dbPath;

#pragma warning disable 8618
        public Context()
        {
            var folder = Environment.CurrentDirectory;
            dbPath = Path.Combine(folder, "smartStat.db");
        }
#pragma warning restore 8618
        public DbSet<UserDb> Users { get; set; }
        public DbSet<PracticeDb> Practices { get; set; }
        public DbSet<StatDb> Stats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}