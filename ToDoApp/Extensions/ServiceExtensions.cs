using Microsoft.EntityFrameworkCore;
using ToDoApp.Core.Repositories;
using ToDoApp.Core.Repositories.Abstractions;
using ToDoApp.Core.Repositories.Contexts;
using ToDoApp.Core.Services;
using ToDoApp.Core.Services.Abstractions;

namespace ToDoApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection, bool useEFInMemory)
        {
            if (useEFInMemory)
            {
                collection.AddDbContext<TodoItemContext>((options) => options.UseInMemoryDatabase("TodoApp"));
                collection.AddScoped<ITodoItemRepository, TodoItemEFInMemRepository>();
            }
            else
            {
                collection.AddSingleton<ITodoItemRepository, TodoItemDictionaryRepository>();
            }
        }
        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddTransient<ITodoItemService, TodoItemService>();
        }

    }
}
