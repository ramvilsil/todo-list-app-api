namespace Api.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TodoListId { get; set; }
}