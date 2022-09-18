using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Data
{
    public class LearningAngularDbContext : DbContext
    {
        public LearningAngularDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
