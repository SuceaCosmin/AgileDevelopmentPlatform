using AgileDevelopmentPlatform.Models;
using AgileDevelopmentPlatform.ViewModel;
using AgileDevelopmentPlatform.ViewModel.Reports;
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
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.SprintList, opt => opt.MapFrom(src => src.Sprints));


            CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.TaskList))
                .ForMember(dest => dest.Sprints, opt => opt.MapFrom(src => src.SprintList));

            CreateMap<Sprint, SprintModel>();
            CreateMap<SprintModel, Sprint>();

            CreateMap<SprintModel, SprintViewModel>();
            CreateMap<SprintViewModel, SprintModel>();

            CreateMap<SprintModel, NewSprintViewModel>();
            CreateMap<NewSprintViewModel, SprintModel>();

            CreateMap<Task, TaskModel>();
            CreateMap<TaskModel, Task>();
            CreateMap<TaskModel, ReferenceTaskViewModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserModel, UserSelectViewModel>();

            CreateMap<TaskModel, EditTaskViewModel>()
                .ForMember(model => model.TaskDificulty, opt => opt.MapFrom(src => src.TaskDificultyId));
            CreateMap<EditTaskViewModel, TaskModel>()
                .ForMember(model => model.TaskDificultyId, opt => opt.MapFrom(src => src.TaskDificulty));
            

            CreateMap<TaskDificultyLevel, TaskDificulty>();
            CreateMap<TaskDificulty, TaskDificultyLevel>();
            CreateMap<TaskDificulty, TaskDificultyViewModel>();
            CreateMap<TaskDificultyViewModel,TaskDificulty>();



            CreateMap<UserAccessModel, UserAccess>();
            CreateMap<UserAccess, UserAccessModel>();

            #region Report mappings

            CreateMap<UserContribution, UserContributionViewModel>();
            CreateMap<TaskStatusModel, TaskStatusReportViewModel>();
            CreateMap<SprintStatusModel, SprintStatusReportViewModel>();

            #endregion

        }
    }
}