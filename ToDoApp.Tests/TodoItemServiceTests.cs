using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Core.Models;
using ToDoApp.Core.Repositories.Abstractions;
using ToDoApp.Core.Services;
using Xunit;

namespace ToDoApp.Tests
{
    public class TodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _todoRepositoryMock;
        private Dictionary<int, TodoItem?> _todos;

        public TodoItemServiceTests()
        {
            _todoRepositoryMock = new Mock<ITodoItemRepository>();
            _todos = new Dictionary<int, TodoItem?>();
            SetupRepositoryMock();
        }
        
        [Fact]
        public async Task GetAll_RetrievesAllTodoItems()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });
            _todos.Add(2, new TodoItem
            {
                Id = 2,
                Title = "Second Todo",
            });

            // Act
            var todoService = GetTodoServiceInstance();
            var retrievedTodos = await todoService.GetAll();

            // Assert
            _todoRepositoryMock.Verify(r => r.GetAll(), Times.Once());
            Assert.Equal(2, retrievedTodos.Count());
        }

        [Fact]
        public async Task GetById_NonExistingTodo_Throws()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            // Assert
            var todoService = GetTodoServiceInstance();
            await Assert.ThrowsAsync<KeyNotFoundException>(() => todoService.GetById(2));
            _todoRepositoryMock.Verify(r => r.GetById(2), Times.Once());
        }

        [Fact]
        public async Task GetById_ExistingTodo_RetrievesTodo()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            var todoService = GetTodoServiceInstance();
            var todo = await todoService.GetById(1);

            // Assert
            _todoRepositoryMock.Verify(r => r.GetById(1), Times.Once());
            Assert.Equal(1, todo.Id);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByCompletion_RetrievesFilteredTodos(bool isCompleted)
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
                IsCompleted = true
            });
            _todos.Add(2, new TodoItem
            {
                Id = 2,
                Title = "Second Todo",
                IsCompleted = false
            });
            // Act
            var todoService = GetTodoServiceInstance();
            var filteredTodos = await todoService.GetByCompletion(isCompleted);

            // Assert
            Assert.Single<TodoItem>(filteredTodos);
        }

        [Fact]
        public async Task DeleteById_NonExistingTodo_Throws()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            // Assert
            var todoService = GetTodoServiceInstance();
            await Assert.ThrowsAsync<KeyNotFoundException>(() => todoService.DeleteById(2));
        }

        [Fact]
        public async Task DeleteById_ExistingTodo_DeletesTodo()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.DeleteById(1);

            // Assert
            _todoRepositoryMock.Verify(r => r.DeleteById(1), Times.Once());
            Assert.Empty(_todos);
        }

        [Fact]
        public async Task DeleteAll_ExistingTodos_DeletesAllTodos()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.DeleteAll();

            // Assert
            _todoRepositoryMock.Verify(r => r.DeleteAll(), Times.Once());
            Assert.Empty(_todos);
        }

        [Fact]
        public async Task Add_NewTodo_AddsTodo()
        {
            // Arrange
            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.Add(new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Assert
            _todoRepositoryMock.Verify(r => r.Add(It.IsAny<TodoItem>()), Times.Once());
            Assert.Single(_todos.Values);
        }

        [Fact]
        public async Task Update_NonExistingTodo_Throws()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
            });

            // Act
            // Assert
            var todoService = GetTodoServiceInstance();
            await Assert.ThrowsAsync<KeyNotFoundException>(() => todoService.Update(2, new TodoItem
            {
                Id = 2,
                Title = "Second Todo"
            }));
        }

        [Fact]
        public async Task Update_ExistingTodo_UpdatesTodo_IgnoresIncomingIdAndCreatedDate()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
                CreatedDate = DateTime.MaxValue
            });

            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.Update(1, new TodoItem
            {
                Id = 999,
                Title = "Updated Todo Title",
                CreatedDate = DateTime.MinValue
            });

            // Assert
            _todoRepositoryMock.Verify(r => r.Update(It.IsAny<TodoItem>()),Times.Once());
            Assert.Equal("Updated Todo Title", _todos[1]?.Title);
            Assert.Equal(1, _todos[1]?.Id);
            Assert.Equal(DateTime.MaxValue, _todos[1]?.CreatedDate);
        }

        [Fact]
        public async Task UpdateCompletionById_NonExistingTodo_Throws()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
                CreatedDate = DateTime.MaxValue
            });

            // Act
            // Assert
            var todoService = GetTodoServiceInstance();
            await Assert.ThrowsAsync<KeyNotFoundException>(() => todoService.UpdateCompletionById(2, true));
        }

        [Fact]
        public async Task UpdateCompletionById_ExistingTodo_UpdatesTodo()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
                IsCompleted = false,
            });

            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.UpdateCompletionById(1, true);

            // Assert
            _todoRepositoryMock.Verify(r => r.Update(It.IsAny<TodoItem>()), Times.Once());
            Assert.True(_todos[1]?.IsCompleted);
        }

        [Fact]
        public async Task UpdateCompletionAll_ExistingTodos_UpdatesAllTodos()
        {
            // Arrange
            _todos.Add(1, new TodoItem
            {
                Id = 1,
                Title = "First Todo",
                IsCompleted = false
            });
            _todos.Add(2, new TodoItem
            {
                Id = 2,
                Title = "Second Todo",
                IsCompleted = false
            });

            // Act
            var todoService = GetTodoServiceInstance();
            await todoService.UpdateCompletionAll(true);

            // Assert
            _todoRepositoryMock.Verify(r => r.UpdateRange(It.IsAny<IEnumerable<TodoItem>>()), Times.Once());
            Assert.Equal(2, _todos.Values.ToList().Where(x => x.IsCompleted).Count());

        }

        private TodoItemService GetTodoServiceInstance()
        {
            return new TodoItemService(_todoRepositoryMock.Object);
        }

        private void SetupRepositoryMock()
        {
            _todoRepositoryMock.Setup(r => r.Add(It.IsAny<TodoItem>()))
                .Callback<TodoItem>( (t) => _todos.Add(t.Id, t));
            _todoRepositoryMock.Setup(r => r.DeleteById(It.IsAny<int>()))
                .Callback<int>((t) => _todos.Remove(t));
            _todoRepositoryMock.Setup(r => r.DeleteAll())
                .Callback(() => _todos.Clear());
            _todoRepositoryMock.Setup(r => r.GetAll())
                .Returns(Task.FromResult(_todos.Values as IEnumerable<TodoItem>));
            _todoRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns<int>((i) => Task.FromResult(_todos.GetValueOrDefault(i)));
            _todoRepositoryMock.Setup(r => r.Update(It.IsAny<TodoItem>()))
                .Callback<TodoItem>((t) => _todos[t.Id] = t);
            _todoRepositoryMock.Setup(r => r.UpdateRange(It.IsAny<IEnumerable<TodoItem>>()))
                .Callback<IEnumerable<TodoItem>>((e) => e.ToList().ForEach((t) => _todos[t.Id] = t));
        }
    }
}