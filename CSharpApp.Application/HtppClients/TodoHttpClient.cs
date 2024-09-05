using CSharpApp.Application.Configuration;
using CSharpApp.Application.HttpClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Application.HtppClients
{
    public class TodoHttpClient : BaseHttpClient<TodoConfiguration>, ITodoHttpClient
    {
        public TodoHttpClient(
            System.Net.Http.HttpClient httpClient, 
            TodoConfiguration configuration, 
            ILogger<BaseHttpClient<TodoConfiguration>> logger) : 
            base(httpClient, configuration, logger)
        {
        }

        public async Task<TodoRecord?> GetTodoById(int id)
            => await Send<TodoRecordResponse, TodoRecord>(b => b.ToEndpoint($"{Configuration.BaseUri}/todos/{id}").
            WithHttpMethod(HttpMethod.Get).
            WithContent(Application.HttpClient.Enums.ContentType.applicationjson).Build());

        public async Task<ReadOnlyCollection<TodoRecord>> GetAllTodos()
        => await Send<TodoRecordResponse, ReadOnlyCollection<TodoRecord>>(b => b.ToEndpoint($"{Configuration.BaseUri}/todos").
            WithHttpMethod(HttpMethod.Get).
            WithContent(Application.HttpClient.Enums.ContentType.applicationjson).Build());

        public async Task<TodoRecord?> AddTodo(AddTodoRecord todoRecord)
        => await Send<AddTodoRecord, TodoRecord>(b => b.ToEndpoint($"{Configuration.BaseUri}/todos").
            WithHttpMethod(HttpMethod.Post).
            WithRequest(todoRecord).
            WithContent(Application.HttpClient.Enums.ContentType.applicationjson).Build());

        public async Task<TodoRecord?> DeleteTodo(int id)
            => await Send<TodoRecordResponse, TodoRecord>(b => b.ToEndpoint($"{Configuration.BaseUri}/todos/{id}").
            WithHttpMethod(HttpMethod.Get).
            WithContent(Application.HttpClient.Enums.ContentType.applicationjson).Build());
    }
}
