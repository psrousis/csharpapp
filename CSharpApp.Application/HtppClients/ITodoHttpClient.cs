using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Application.HtppClients
{
    public interface ITodoHttpClient
    {
        Task<TodoRecord?> GetTodoById(int id);
        Task<ReadOnlyCollection<TodoRecord>> GetAllTodos();
        Task<TodoRecord?> AddTodo(AddTodoRecord todoRecord);
        Task<TodoRecord?> DeleteTodo(int id);
    }
}
