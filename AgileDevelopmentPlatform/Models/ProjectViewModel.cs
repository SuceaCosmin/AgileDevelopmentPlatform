using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskModel> TaskList { get; set; }
    }

    public class ProjectCreateOrEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }

    public class NewTaskViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }

        public int TaskId { get; set; }

        public int ProjectID { get; set; }
    }
}