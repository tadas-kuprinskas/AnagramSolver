using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.DatabaseFirst.Data
{
    public partial class AnagramSolverContext : DbContext
    {
        public AnagramSolverContext()
        {
        }

        public AnagramSolverContext(DbContextOptions<AnagramSolverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; }
        public virtual DbSet<SearchInformation> SearchInformations { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordCachedWordAdditional> WordCachedWordAdditionals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=AnagramSolver;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.ToTable("Cached_Words");

                entity.HasIndex(e => e.Value, "UQ__Cached_W__07D9BBC2DA4196E2")
                    .IsUnique();

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SearchInformation>(entity =>
            {
                entity.Property(e => e.Anagram).HasMaxLength(50);

                entity.Property(e => e.SearchTime).HasColumnType("datetime");

                entity.Property(e => e.SearchedWord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.OrderedValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartOfSpeech)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WordCachedWordAdditional>(entity =>
            {
                entity.ToTable("Word_CachedWord_Additionals");

                entity.HasOne(d => d.CachedWord)
                    .WithMany(p => p.WordCachedWordAdditionals)
                    .HasForeignKey(d => d.CachedWordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Word_CachedWord_Additionals_Cached_Words");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.WordCachedWordAdditionals)
                    .HasForeignKey(d => d.WordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Word_CachedWord_Additionals_Words");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
