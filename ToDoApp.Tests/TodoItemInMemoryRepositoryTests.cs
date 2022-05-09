using System;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.Core.Repositories;
using ToDoApp.Core.Repositories.Abstractions;
using Xunit;

namespace ToDoApp.Tests
{
    public class TodoItemInMemoryRepositoryTests
    {
        private readonly ITodoItemRepository _todoRepository;

        public TodoItemInMemoryRepositoryTests()
        {
            _todoRepository = new TodoItemInMemoryRepository();
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
        }
    }
}
