using CSharpApp.Application.Configuration;
using CSharpApp.Application.HtppClients;
using CSharpApp.Core.Dtos;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<TodoConfiguration>(builder.Configuration.GetSection("TodoConfiguration"));

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<TodoConfiguration>>().Value);

builder.Services.AddHttpClient<TodoHttpClient>();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAllTodos();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetTodoById(id);
        return todos;
    })
    .WithName("GetTodosById")
    .WithOpenApi();

app.MapPost("/todos", async ([FromBody] AddTodoRecord todoRecord, ITodoService todoService) =>
{
    var todos = await todoService.AddTodo(todoRecord);
    return todos;
})
    .WithName("AddTodo")
    .WithOpenApi();

app.MapDelete("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
{
    var todos = await todoService.DeleteTodo(id);
    return todos;
})
    .WithName("DeleteTodo")
    .WithOpenApi();

app.Run();