using ToDoApp.Models;

namespace ToDoApp.Services.Abstractions
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<IEnumerable<TodoItem>> GetByCompletion(bool isCompleted);
        Task<TodoItem> GetById(int id);
        Task DeleteById(int id);
        Task DeleteAll();
        Task<TodoItem> Add(TodoItem todoItem);
        Task<TodoItem> Update(int id, TodoItem todoItem);
        Task Complete(int id);
        Task CompleteAll();
    }
}
