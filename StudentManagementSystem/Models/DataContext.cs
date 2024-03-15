using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<StudentManagementSystem.Models.Login> Login { get; set; }
        public DbSet<StudentManagementSystem.Models.ChangePassword> ChangePassword { get; set; }
    }
}
