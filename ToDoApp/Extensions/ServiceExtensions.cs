using ToDoApp.Repositories;
using ToDoApp.Repositories.Abstractions;
using ToDoApp.Services;
using ToDoApp.Services.Abstractions;

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
