
using AutoMapper;
using FirstApi.DTOs;
using FirstApi.Models;

namespace FirstApi.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskDto>();

            CreateMap<CreateTaskDto, TaskItem>();

            CreateMap<UpdateTaskDto, TaskItem>();
        }
    }
}


