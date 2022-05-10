using System.Collections.Concurrent;
using ToDoApp.Core.Models;
using ToDoApp.Core.Repositories.Abstractions;

namespace ToDoApp.Core.Repositories
{
    public class TodoItemInMemoryRepository : ITodoItemRepository
    {
        private static int _lastId = 0;
        private static readonly ConcurrentDictionary<int, TodoItem> _todosMap = new ConcurrentDictionary<int, TodoItem>();

        public Task<TodoItem> Add(TodoItem todoItem)
        {
            todoItem.Id = GenerateId();
            todoItem.CreatedDate = DateTime.Now;
            _todosMap[todoItem.Id] = todoItem;
            return Task.FromResult(todoItem);
        }

        public Task DeleteAll()
        {
            _todosMap.Clear();
            return Task.CompletedTask;
        }

        public Task DeleteById(int id)
        {
            _todosMap.Remove(id, out _);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return Task.FromResult(_todosMap.Values as IEnumerable<TodoItem>);
        }

        public Task<TodoItem?> GetById(int id)
        {
            return Task.FromResult(_todosMap.TryGetValue(id, out var todo) ? todo : default);
        }

        public Task<TodoItem> Update(TodoItem todoItem)
        {
            _todosMap[todoItem.Id] = todoItem;
            return Task.FromResult(todoItem);
        }

        public Task<IEnumerable<TodoItem>> UpdateRange(IEnumerable<TodoItem> todoItems)
        {
            foreach (var todoItem in todoItems)
            {
                _todosMap[todoItem.Id] = todoItem;
            }
            return Task.FromResult(todoItems);
        }

        private static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}
