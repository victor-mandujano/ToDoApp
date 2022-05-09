using ToDoApp.Core.Repositories;
using ToDoApp.Core.Repositories.Abstractions;
using ToDoApp.Core.Services;
using ToDoApp.Core.Services.Abstractions;

namespace ToDoApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddSingleton<ITodoItemRepository, TodoItemInMemoryRepository>();
        }
        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddTransient<ITodoItemService, TodoItemService>();
        }

    }
}
