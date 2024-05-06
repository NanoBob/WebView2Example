using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Web.Persistence;

public class TodoContext : DbContext
{
    public DbSet<TodoItem> Items { get; set; }

    public TodoContext()
    {
        this.Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("Data source=data.db");
    }
}
