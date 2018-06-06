﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{


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

        public string OwnerId { get; set; }
    

        public List<SprintModel> SprintList { get; set; }

        public List<TaskModel> TaskList { get; set; }   

    }
}