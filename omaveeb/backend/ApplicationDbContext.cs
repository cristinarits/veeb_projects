using Microsoft.EntityFrameworkCore;
using orm.Models;
using kontrolltoo.Models;

namespace orm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestScore> TestScores { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<TestScore>().ToTable("TestScore");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
        }
    }
}
