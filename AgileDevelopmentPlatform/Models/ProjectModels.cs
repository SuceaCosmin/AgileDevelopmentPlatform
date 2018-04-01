using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class TaskPriority
    {
        public static readonly string Low = "Low";
        public static readonly string Medium = "Medium";
        public static readonly string High = "High";

        public static List<string> List {
            get
            {
                List < string > priorityList= new List<string>();
                priorityList.Add(Low);
                priorityList.Add(Medium);
                priorityList.Add(High);
                return priorityList;
            }
            
        } 
    }

    public class TaskState
    {
        public static readonly string Open = "Open";
        public static readonly string Working = "Working";
        public static readonly string Review = "Review";
        public static readonly string Finished = "Finished";

        public static List<string> List
        {
           
            get
            {
                List<string> stateList = new List<string>();
                stateList.Add(Open);
                stateList.Add(Working);
                stateList.Add(Review);
                stateList.Add(Finished);
                return stateList;
            }

        }
    }

    public class TaskModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int? SprintId { get; set; }

        public int TaskInitiatorId { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }    

        public int ResponsibleUserId { get; set; }

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

        public List<SprintModel> SprintList { get; set; }

        public List<TaskModel> TaskList { get; set; }   

    }
}