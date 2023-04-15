namespace Api.Models;

public class TodoList
{
    public Guid Id { get; set; }
    public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}