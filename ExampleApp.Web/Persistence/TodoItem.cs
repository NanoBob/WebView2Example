namespace ExampleApp.Web.Persistence;

public class TodoItem
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; } = false;
}
