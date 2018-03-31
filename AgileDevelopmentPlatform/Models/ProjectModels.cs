using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class TaskPriorities
    {
        public static List<string> PriorityList {
            get
            {
                List < string > priorityList= new List<string>();
                priorityList.Add("Low");
                priorityList.Add("Medium");
                priorityList.Add("High");
                return priorityList;
            }
            
        } 
    }

    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public string Status { get; set; }

        public int ProjectId { get; set; }
    }

    public class ProjectModel
    {

        public ProjectModel()
        {
            Id = 0;
        }

        public ProjectModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<TaskModel> TaskList { get; set; }   

    }
}