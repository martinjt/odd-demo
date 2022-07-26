#define First // LAST First
#if First
#region snippet_first
using honeycomb_odd.Models;
using Microsoft.EntityFrameworkCore;

namespace honeycomb_odd.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
#endregion
#elif LAST
#endif