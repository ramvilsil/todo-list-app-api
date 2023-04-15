using Api.Models;

namespace Api.Services;

public interface ITodoItemService
{
    Task<TodoItem> CreateAsync(Guid todoListId, string todoItemName);
    Task UpdateAsync(string todoItemId, string newTodoItemName);
    Task DeleteAsync(string todoItemId);
}