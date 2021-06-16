using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnagramSolver.EF.CodeFirst.Data
{
    public class AnagramSolverCodeFirstContext : DbContext
    {
        public AnagramSolverCodeFirstContext()
        {
        }

        public AnagramSolverCodeFirstContext(DbContextOptions<AnagramSolverCodeFirstContext> options)
            : base(options) { }

        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<SearchInformation> SearchInformations { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordCachedWordAdditional> WordCachedWordAdditionals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Anagram_Solver;Trusted_Connection=True;");
            }
        }

    }
}
