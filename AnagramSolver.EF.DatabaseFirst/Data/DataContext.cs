using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.DatabaseFirst.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<CachedWord> Cached_Words { get; set; }
        public DbSet<Word_CachedWord_Additionals> Word_CachedWord_Additionals { get; set; }
        public DbSet<SearchInformation> SearchInformations { get; set; }
    }
}
