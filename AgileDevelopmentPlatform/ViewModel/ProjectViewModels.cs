using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgileDevelopmentPlatform.ViewModel
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public IEnumerable<SprintViewModel> SprintList { get; set; }

        public IEnumerable<ReferenceTaskViewModel> TaskList { get; set; }
    }

    public class ProjectCreateOrEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string OwnerId { get; set; }
    }

    public class ProjectAccessViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public List<UserSelectViewModel> GrantedAccessList { get; set; }
    }

    public class ProjectUserAccessViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public List<UserSelectViewModel> UserList { get; set; }

    }

}