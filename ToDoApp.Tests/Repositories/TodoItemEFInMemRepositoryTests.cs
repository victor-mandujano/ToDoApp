using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.Core.Repositories;
using ToDoApp.Core.Repositories.Abstractions;
using ToDoApp.Core.Repositories.Contexts;
using Xunit;

namespace ToDoApp.Tests.Repositories
{
    // TODO: Add more unit tests for this repository.
    public class TodoItemEFInMemRepositoryTests
    {
        private readonly ITodoItemRepository _todoRepository;
        public TodoItemEFInMemRepositoryTests()
        {
            _todoRepository = new TodoItemEFInMemRepository(GetTodoContext());
        }

        [Fact]
        public async Task Add_IgnoresIncomingIdAndCreatedDate()
        {
            // Arrange
            // Act
            var addedTodo = await _todoRepository.Add(new TodoItem
            {
                Id = 999,
                Title = "First Todo Item",
                CreatedDate = DateTime.MinValue,
                IsCompleted = true
            });

            // Assert
            Assert.NotEqual(999, addedTodo.Id);
            Assert.NotEqual(DateTime.MinValue, addedTodo.CreatedDate);
            Assert.Equal("First Todo Item", addedTodo.Title);
            Assert.True(addedTodo.IsCompleted);
            Assert.Single<TodoItem>(await _todoRepository.GetAll());
        }

        private TodoItemContext GetTodoContext()
        {
            var options = new DbContextOptionsBuilder<TodoItemContext>()
                .UseInMemoryDatabase(databaseName: "TodoApp")
                .Options;
            var todoContext = new TodoItemContext(options);
            todoContext.Database.EnsureCreated();
            return todoContext;
        }
    }
}
