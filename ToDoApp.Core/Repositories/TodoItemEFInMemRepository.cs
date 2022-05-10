using ToDoApp.Core.Models;
using ToDoApp.Core.Repositories.Abstractions;
using ToDoApp.Core.Repositories.Contexts;

namespace ToDoApp.Core.Repositories
{
    public class TodoItemEFInMemRepository : ITodoItemRepository
    {
        private static int _lastId = 0;
        private readonly TodoItemContext _context;

        public TodoItemEFInMemRepository(TodoItemContext todoContext)
        {
            _context = todoContext;
        }

        public async Task<TodoItem> Add(TodoItem todoItem)
        {
            todoItem.Id = GenerateId();
            todoItem.CreatedDate = DateTime.Now;
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return todoItem;
        }

        public async Task DeleteAll()
        {
            foreach (var todo in _context.TodoItems)
            {
                _context.TodoItems.Remove(todo);
            }
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteById(int id)
        {
            var todo = await GetById(id);
            if (todo == null)
            {
                throw new KeyNotFoundException();
            }
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return Task.FromResult(_context.TodoItems.ToList() as IEnumerable<TodoItem>);
        }

        public async Task<TodoItem?> GetById(int id)
        {
            return await _context.TodoItems.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<TodoItem> Update(TodoItem todoItem)
        {
            var existingTodo = await GetById(todoItem.Id);
            if (existingTodo == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Entry(existingTodo).CurrentValues.SetValues(todoItem);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> UpdateRange(IEnumerable<TodoItem> todoItems)
        {
            foreach (var todo in todoItems)
            {
                await Update(todo);
            }
            return todoItems;
        }

        private static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}