using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITodoListService _todoListService;
    public TodoListController
    (
        ILogger<TodoListController> logger,
        IHttpContextAccessor httpContextAccessor,
        ITodoListService todoListService
    )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _todoListService = todoListService;
    }

    [HttpGet(Name = "GetTodoList")]
    public TodoList Get()
    {

        if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("todoListId", out string todoListId))
        {
            todoListId = _httpContextAccessor.HttpContext.Request.Cookies["todoListId"];

            var todoList = _todoListService.GetById(Guid.Parse(todoListId));

            return todoList;
        }

        return new TodoList();
    }
}