using System.Web.ClientServices.Providers;
using AgileDevelopmentPlatform.Models;
using AutoMapper;

namespace AgileDevelopmentPlatform
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            //Project related mappings
            CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.Tasks));
                
            CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.ProjectName,opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.TaskList));

            CreateMap<Task, TaskModel>();
            CreateMap<TaskModel, Task>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<TaskModel, EditTaskViewModel>();
            CreateMap<EditTaskViewModel, TaskModel>();
        }
    }
}