using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace orm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestScore> TestScores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}