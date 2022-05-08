using ToDoApp.Models;

namespace ToDoApp.Services.Abstractions
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<IEnumerable<TodoItem>> GetCompleted();
        Task<TodoItem> GetById(int id);
        Task DeleteById(int id);
        Task DeleteAll();
        Task Add(TodoItem todoItem);
        Task<TodoItem> Update(int id, TodoItem todoItem);
        Task Complete(int id);
        Task CompleteAll();
    }
}
