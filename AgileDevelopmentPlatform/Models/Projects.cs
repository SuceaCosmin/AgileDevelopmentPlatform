using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class Projects
    {

        public Projects()
        {
            List=new List<ProjectModel>();
        }

        public void Add(ProjectModel project)
        {
            List.Add(project);
        }

        public List<ProjectModel> List { get; set; }
    }
}