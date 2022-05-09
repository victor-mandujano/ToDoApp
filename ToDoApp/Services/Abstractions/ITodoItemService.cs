using ToDoApp.Models;

namespace ToDoApp.Services.Abstractions
{
    /// <summary>
    /// Service to manage <see cref="TodoItem">TodoItem</see> instances.
    /// </summary>
    public interface ITodoItemService
    {
        /// <summary>
        /// Get all instances of <see cref="TodoItem">TodoItem</see>.</see>
        /// </summary>
        Task<IEnumerable<TodoItem>> GetAll();

        /// <summary>
        /// Get all instances of <see cref="TodoItem">TodoItem</see> with the specified completion status.
        /// </summary>
        /// <param name="isCompleted">Whether the <see cref="TodoItem">TodoItem</see> is completed</param>
        Task<IEnumerable<TodoItem>> GetByCompletion(bool isCompleted);

        /// <summary>
        /// Gets the instance of <see cref="TodoItem">TodoItem</see> with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the <see cref="TodoItem">TodoItem</see> to retrieve.</param>
        Task<TodoItem> GetById(int id);

        /// <summary>
        /// Deletes the instance of <see cref="TodoItem">TodoItem</see> with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the <see cref="TodoItem">TodoItem</see> to delete.</param>
        Task DeleteById(int id);

        /// <summary>
        /// Deletes all isntances of <see cref="TodoItem">TodoItem</see>.
        /// </summary>
        Task DeleteAll();

        /// <summary>
        /// Add a new instance of <see cref="TodoItem">TodoItem</see>.
        /// </summary>
        /// <param name="todoItem"> The new instance of <see cref="TodoItem">TodoItem</see> to add.</param>
        Task<TodoItem> Add(TodoItem todoItem);

        /// <summary>
        /// Updates an existing instance of <see cref="TodoItem">TodoItem</see>.
        /// </summary>
        /// <param name="id">The Id of the existing instance of <see cref="TodoItem">TodoItem</see>.</param>
        /// <param name="todoItem">The instance of <see cref="TodoItem">TodoItem</see> with updated values to add.</param>
        Task<TodoItem> Update(int id, TodoItem todoItem);

        /// <summary>
        /// Updates the completion status of the <see cref="TodoItem">TodoItem</see> instance with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the existing instane of <see cref="TodoItem">TodoItem</see>.</param>
        /// <param name="isCompleted">The new completion status.</param>
        Task<TodoItem> UpdateCompletionById(int id, bool isCompleted);

        /// <summary>
        /// Updates the completion status of all <see cref="TodoItem">TodoItem</see> instances.
        /// </summary>
        /// <param name="isCompleted">The new completion status.</param>
        Task<IEnumerable<TodoItem>> UpdateCompletionAll(bool isCompleted);
    }
}
