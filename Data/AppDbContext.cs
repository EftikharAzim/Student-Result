using Microsoft.EntityFrameworkCore;
using StudentCourseResults.Models;

namespace StudentCourseResults.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<StudentResult> StudentResults { get; set; }
    }
}