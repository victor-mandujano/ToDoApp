using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Core.DataContracts
{
    public class TodoItemDto
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
