using AutoMapper;
using ToDoApp.DataContracts;
using ToDoApp.Models;

namespace ToDoApp.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();
            CreateMap<TodoItemDto, TodoItem>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.Ignore());
            CreateMap<TodoItem, TodoItemDto>();
        }
    }
}
