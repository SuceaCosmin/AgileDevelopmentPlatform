using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace AgileDevelopmentPlatform.Models
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Project, ProjectModel>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.ProjectName))
                .ReverseMap();

        }
    }
}