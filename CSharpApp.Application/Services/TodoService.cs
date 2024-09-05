using CSharpApp.Application.HtppClients;

namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly TodoHttpClient todoHttpClient;


    public TodoService(ILogger<TodoService> logger, TodoHttpClient todoHttpClient)
    {
        _logger = logger;
        this.todoHttpClient = todoHttpClient;
    }

    public async Task<TodoRecord?> GetTodoById(int id)
    {
        var response = await todoHttpClient.GetTodoById(id);

        return response;
    }

    public async Task<ReadOnlyCollection<TodoRecord>> GetAllTodos()
    {
        var response = await todoHttpClient.GetAllTodos();

        return response!.AsReadOnly();
    }

    public async Task<TodoRecord?> AddTodo(AddTodoRecord todoRecord)
    {
        var response = await todoHttpClient.AddTodo(todoRecord);

        return response;
    }

    public async Task<TodoRecord?> DeleteTodo(int id)
    {
        var response = await todoHttpClient.DeleteTodo(id);

        return response;
    }
}