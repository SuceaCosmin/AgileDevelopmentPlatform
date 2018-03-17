using AgileDevelopmentPlatform.Models;
using AutoMapper;

namespace AgileDevelopmentPlatform
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName));

            CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.ProjectName,opt=>opt.MapFrom(src=>src.Name));

        }
    }
}