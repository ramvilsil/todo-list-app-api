using Api.Models;

namespace Api.Services;

public interface ITodoListService
{
    Task<TodoList> CreateAsync();
    TodoList? GetById(Guid todoListId);
}