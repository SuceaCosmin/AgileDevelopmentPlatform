using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AgileDevelopmentPlatform.ViewModel
{
    public class NewTaskViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }
        public IEnumerable<string> PriorityType { get; set; }

        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        public int EffortEstimation { get; set; }

        [Required]
        [Display(Name = "Dificulty")]
        public int TaskDificulty { get; set; }
        public IEnumerable<TaskDificultyViewModel> TaskDificultyList { get; set; }

        public DateTime TaskCreationDate { get; set; }

    }

    public class EditTaskViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Task initiator")]
        public string TaskInitiatorId { get; set; }

        [Display(Name = "Responsible user")]
        public string ResponsibleUserId { get; set; }
        public IEnumerable<UserSelectViewModel> UserList { get; set; }

        [Required]
        public string Priority { get; set; }
        public IEnumerable<string> PriorityType { get; set; }

        [Required]
        public string Status { get; set; }
        public IEnumerable<string> TaskStateList { get; set; }

        public int EffortEstimation { get; set; }

        [Display(Name="Work effort(h)")]
        public int WorkEffort { get; set; }

        [Required]
        [Display(Name = "Dificulty")]
        public int TaskDificulty { get; set; }
        public IEnumerable<TaskDificultyViewModel> TaskDificultyList { get; set; }

        public DateTime TaskCreationDate { get; set; }

        public DateTime TaskCompletionDate { get; set; }


    }

    public class ReferenceTaskViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public bool IsCompleted { get; set; }
    }

    public class AssignTaskToSprintViewModel
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        public int SprintId { get; set; }
        public IEnumerable<SelectListItem> SprintList { get; set; }
    }
}