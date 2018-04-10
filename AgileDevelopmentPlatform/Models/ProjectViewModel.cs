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

        public IEnumerable<SprintViewModel> SprintList { get; set; }

        public IEnumerable<ReferenceTaskViewModel> TaskList { get; set; }
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

        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        
        public string Priority { get; set; }
        public IEnumerable<string> PriorityType { get; set; }

        public int TaskId { get; set; }

        public int ProjectId { get; set; }


    }

    public class EditTaskViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? TaskInitiatorId { get; set; }

        public int ResponsibleUserId { get; set; }


        public IEnumerable<UserSelectViewModel> UserList { get; set; }

        [Required]
        public string Priority { get; set; }
        public IEnumerable<string> PriorityType { get; set; }

        [Required]
        public string Status { get; set; }
        public IEnumerable<string> TaskStateList { get; set; }


    }

    public class ReferenceTaskViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}