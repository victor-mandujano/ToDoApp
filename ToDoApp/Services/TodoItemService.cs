using ToDoApp.Models;
using ToDoApp.Repositories.Abstractions;
using ToDoApp.Services.Abstractions;

namespace ToDoApp.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoRepository = todoItemRepository;  
        }

        public Task<TodoItem> Add(TodoItem todoItem)
        {
            return _todoRepository.Add(todoItem);
        }

        public Task Complete(int id)
        {
            throw new NotImplementedException();
        }

        public Task CompleteAll()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return _todoRepository.GetAll();
        }

        public Task<TodoItem> GetById(int id)
        {
            var todo = _todoRepository.GetById(id);
            if (todo == null)
            {
                throw new KeyNotFoundException();
            }
            return todo;
        }

        public async Task<IEnumerable<TodoItem>> GetByCompletion(bool isCompleted)
        {
            var allTodos = await _todoRepository.GetAll().ConfigureAwait(false);
            return allTodos.Where(t => t.IsCompleted == isCompleted);
        }

        public async Task<TodoItem> Update(int id, TodoItem todoItem)
        {
            var existingTodo = await _todoRepository.GetById(id).ConfigureAwait(false);
            if (existingTodo == null)
            {
                throw new KeyNotFoundException();
            }
            todoItem.Id = existingTodo.Id;
            todoItem.CreatedDate = existingTodo.CreatedDate;
            var updatedTodo = await _todoRepository.Update(id, todoItem);
            return updatedTodo;
        }
    }
}
