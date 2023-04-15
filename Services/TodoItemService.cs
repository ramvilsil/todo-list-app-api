using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Services;

public class TodoItemService : ITodoItemService
{
    private readonly Data.DbContext _context;

    public TodoItemService
    (
        Data.DbContext context
    )
    {
        _context = context;
    }

    public async Task<TodoItem> CreateAsync(Guid todoListId, string todoItemName)
    {
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Name = todoItemName,
            TodoListId = todoListId
        };

        await _context.TodoItems.AddAsync(todoItem);

        await _context.SaveChangesAsync();

        return todoItem;
    }

    public async Task UpdateAsync(string todoItemId, string newTodoItemName)
    {
        var todoItem = await _context.TodoItems.FindAsync(Guid.Parse(todoItemId));
        if (todoItem != null)
        {
            todoItem.Name = newTodoItemName;
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(string todoItemId)
    {
        var todoItem = await _context.TodoItems.FindAsync(Guid.Parse(todoItemId));
        if (todoItem != null)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}