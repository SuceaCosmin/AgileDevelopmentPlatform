﻿using System.Web.ClientServices.Providers;
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
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.SprintList, opt => opt.MapFrom(src => src.Sprints));


            CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.TaskList))
                .ForMember(dest => dest.Sprints, opt => opt.MapFrom(src => src.SprintList));

            CreateMap<Sprint, SprintModel>();
            CreateMap<SprintModel, Sprint>();

            CreateMap<SprintModel, SprintViewModel>();

            CreateMap<Task, TaskModel>();
            CreateMap<TaskModel, Task>();
            CreateMap<TaskModel, ReferenceTaskViewModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<TaskModel, EditTaskViewModel>();
            CreateMap<EditTaskViewModel, TaskModel>();
        }
    }
}