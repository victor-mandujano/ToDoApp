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

        public async Task<TodoItem> UpdateCompletionById(int id, bool isCompleted)
        {
            var todo = await _todoRepository.GetById(id).ConfigureAwait(false);
            if (todo == null)
            {
                throw new KeyNotFoundException();
            }
            todo.IsCompleted = isCompleted;
            return await _todoRepository.Update(todo).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TodoItem>> UpdateCompletionAll(bool isCompleted)
        {
            var todos = await _todoRepository.GetAll().ConfigureAwait(false);
            foreach (var todo in todos)
            {
                todo.IsCompleted = isCompleted;
            }
            return await _todoRepository.UpdateRange(todos).ConfigureAwait(false);
        }

        public Task DeleteAll()
        {
            return _todoRepository.DeleteAll();
        }

        public async Task DeleteById(int id)
        {
            var todo = await _todoRepository.GetById(id).ConfigureAwait(false);
            if (todo == null)
            {
                throw new KeyNotFoundException();
            }
            await _todoRepository.DeleteById(todo.Id).ConfigureAwait(false);
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
            var updatedTodo = await _todoRepository.Update(todoItem).ConfigureAwait(false);
            return updatedTodo;
        }
    }
}
