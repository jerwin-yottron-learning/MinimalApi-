using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace TodoApp
{
    public class TodoDBContext:DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options):base(options)
        { }

        public DbSet<TodoModel> Todos { get; set; }
    }
}
