using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class SprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

        public List<ReferenceTaskViewModel> Tasks { get; set; }

        public int OpenTasks { get; set; }

        public int WorkingTasks { get; set; }

        public int FinishedTasks { get; set; }
    }

    public class NewSprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

    }
}