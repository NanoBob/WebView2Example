using ExampleApp.Web.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController(TodoContext context) : ControllerBase
{
    [HttpGet("")]
    public async Task<IEnumerable<TodoItem>> GetItems()
    {
        return await context.Items.ToArrayAsync();
    }

    [HttpPost("")]
    public async Task<TodoItem> CreateItem([FromBody] TodoItem item)
    {
        context.Items.Add(item);
        await context.SaveChangesAsync();

        return item;
    }

    [HttpPatch("{id}")]
    public async Task CompleteItem(Guid id, [FromBody] bool isCompleted = true)
    {
        var item = await context.Items.FindAsync(id);
        if (item == null)
            return;

        item.IsCompleted = isCompleted;

        context.Update(item);
        await context.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeleteItem(Guid id)
    {
        var item = await context.Items.FindAsync(id);
        if (item == null)
            return;

        context.Items.Remove(item);
        await context.SaveChangesAsync();
    }
}
