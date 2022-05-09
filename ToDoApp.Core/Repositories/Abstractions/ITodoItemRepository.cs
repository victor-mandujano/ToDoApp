using ToDoApp.Core.Models;

namespace ToDoApp.Core.Repositories.Abstractions
{
    /// <summary>
    /// Repository to manage <see cref="TodoItem">TodoItem</see> instances./>
    /// </summary>
    public interface ITodoItemRepository
    {
        /// <summary>
        /// Get all <see cref="TodoItem">TodoItem</see> instances.
        /// </summary>
        Task<IEnumerable<TodoItem>> GetAll();

        /// <summary>
        /// Get the <see cref="TodoItem">TodoItem</see> instance with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the <see cref="TodoItem">TodoItem</see> instance to get.</param>
        Task<TodoItem> GetById(int id);

        /// <summary>
        /// Delete the <see cref="TodoItem">TodoItem</see> instance with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the <see cref="TodoItem">TodoItem</see> instance to delete.</param>
        Task DeleteById(int id);

        /// <summary>
        /// Delete all <see cref="TodoItem">TodoItem</see> instances.
        /// </summary>
        Task DeleteAll();

        /// <summary>
        /// Add a new instance of <see cref="TodoItem">TodoItem</see>.
        /// </summary>
        /// <param name="todoItem">The <see cref="TodoItem">TodoItem</see> instance to add.</param>
        Task<TodoItem> Add(TodoItem todoItem);

        /// <summary>
        /// Update an existing instance of <see cref="TodoItem">TodoItem</see>.
        /// </summary>
        /// <param name="todoItem">The updated <see cref="TodoItem">TodoItem</see> instance.</param>
        Task<TodoItem> Update(TodoItem todoItem);

        /// <summary>
        /// Update a range of <see cref="TodoItem">TodoItem</see> instances.
        /// </summary>
        /// <param name="todoItems">An <see cref="IEnumerable{T}">IEnumerable</see> containing <see cref="TodoItem">TodoItem</see> instances to update.</param>
        Task<IEnumerable<TodoItem>> UpdateRange(IEnumerable<TodoItem> todoItems);

    }
}
