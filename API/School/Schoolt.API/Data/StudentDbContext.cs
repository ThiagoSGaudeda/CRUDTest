using Microsoft.EntityFrameworkCore;
using Schoolt.API.Models;

namespace Schoolt.API.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }

        //DBset
        public DbSet<Student> Students { get; set; }
    }
}
