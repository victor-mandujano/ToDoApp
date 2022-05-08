namespace ToDoApp.Models
{
    public class TodoItem
    {
        int Id { get; set; }
        string? Title { get; set; }
        string? Description { get; set; }
        bool IsCompleted { get; set; }
        DateTime CreatedDate { get; set; }

    }
}
