using System.Runtime.Serialization;

namespace ToDoApp.DataContracts
{
    [DataContract(Name ="TodoItem")]
    public class TodoItemDto
    {
        [DataMember(IsRequired = true)]
        int Id { get; set; }

        [DataMember(IsRequired = true)]
        string? Title { get; set; }
        [DataMember]
        string? Description { get; set; }
        [DataMember(IsRequired = true)]
        bool IsCompleted { get; set; }
        [DataMember]
        DateTime CreatedDate { get; set; }
    }
}
