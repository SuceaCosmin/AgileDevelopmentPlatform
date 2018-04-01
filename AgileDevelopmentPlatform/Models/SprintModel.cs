using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class SprintModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

        public List<TaskModel> Tasks { get; set; }

    }
}