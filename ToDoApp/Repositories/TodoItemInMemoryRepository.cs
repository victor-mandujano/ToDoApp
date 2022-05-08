using System.Collections;
using System.Collections.Concurrent;
using ToDoApp.Models;
using ToDoApp.Repositories.Abstractions;

namespace ToDoApp.Repositories
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
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return Task.FromResult(_todosMap.Values as IEnumerable<TodoItem>);
        }

        public Task<TodoItem> GetById(int id)
        {
            return Task.FromResult(_todosMap[id]);
        }

        public Task<TodoItem> Update(int id, TodoItem todoItem)
        {
            _todosMap[id] = todoItem;
            return Task.FromResult(todoItem);
        }

        private static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}
