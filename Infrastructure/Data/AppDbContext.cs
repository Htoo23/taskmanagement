

using Microsoft.EntityFrameworkCore;
using TaskmanagementSystem.Core.Entities;

namespace TaskmanagementSystem.Infrastructure.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }


    }
}
