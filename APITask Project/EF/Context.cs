using APITask_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APITask_Project.EF
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<MyTask> MyTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-32KKTDQ;Database=Rira2;Integrated Security=True;");
        }

    }
}
