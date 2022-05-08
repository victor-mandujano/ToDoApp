using ToDoApp.Models;

namespace ToDoApp.Repositories.Abstractions
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(int id);
        Task DeleteById(int id);
        Task DeleteAll();
        Task<TodoItem> Add(TodoItem todoItem);
        Task<TodoItem> Update(int id, TodoItem todoItem);

    }
}
