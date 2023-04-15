using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.DTOs;
using Api.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly ILogger<TodoItemController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITodoItemService _todoItemService;
    private readonly ITodoListService _todoListService;
    public TodoItemController
    (
        ILogger<TodoItemController> logger,
        IHttpContextAccessor httpContextAccessor,
        ITodoItemService todoItemService,
        ITodoListService todoListService
    )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _todoItemService = todoItemService;
        _todoListService = todoListService;
    }

    [HttpPost(Name = "CreateTodoItem")]
    public async Task<IActionResult> Create([FromBody] TodoItemRequest todoItemRequest)
    {
        TodoList todoList;

        if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("todoListId", out string todoListId))
        {
            todoListId = _httpContextAccessor.HttpContext.Request.Cookies["todoListId"];

            todoList = _todoListService.GetById(Guid.Parse(todoListId));
        }
        else
        {
            todoList = await _todoListService.CreateAsync();

            _httpContextAccessor.HttpContext.Response.Cookies.Append("todoListId", todoList.Id.ToString(), new CookieOptions()
            {
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(30)
            });
        }

        await _todoItemService.CreateAsync(todoList.Id, todoItemRequest.TodoItemName);

        return Ok();
    }

    [HttpPut(Name = "UpdateTodoItem")]
    public async Task<IActionResult> Update([FromBody] TodoItemRequest todoItemRequest)
    {
        await _todoItemService.UpdateAsync(todoItemRequest.TodoItemId, todoItemRequest.TodoItemName);

        return Ok();
    }

    [HttpDelete(Name = "DeleteTodoItem")]
    public async Task<IActionResult> Delete([FromBody] TodoItemRequest todoItemRequest)
    {
        await _todoItemService.DeleteAsync(todoItemRequest.TodoItemId);

        return Ok();
    }
}