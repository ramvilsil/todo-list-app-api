using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Services;

public class TodoListService : ITodoListService
{
    private readonly Data.DbContext _context;
    public TodoListService
    (
        Data.DbContext context
    )
    {
        _context = context;
    }

    public async Task<TodoList> CreateAsync()
    {
        var todoList = new TodoList() { Id = Guid.NewGuid() };

        await _context.TodoLists.AddAsync(todoList);
        await _context.SaveChangesAsync();

        return todoList;
    }

    public TodoList? GetById(Guid todoListId)
    {
        var todoList = _context.TodoLists.Include(x => x.TodoItems).FirstOrDefault(x => x.Id == todoListId);

        return todoList;
    }
}