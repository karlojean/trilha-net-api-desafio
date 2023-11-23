using Microsoft.EntityFrameworkCore;
using TaskModel = TrilhaApiDesafio.Models.Task;

namespace TrilhaApiDesafio.Context
{
    public class OrganizerContext : DbContext
    {
        public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options)
        {
            
        }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}