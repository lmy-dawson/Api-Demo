using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace StudentApi.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) :base(options)
        { 

        }

         public DbSet<Student> Students { get; set; }
    }
}
