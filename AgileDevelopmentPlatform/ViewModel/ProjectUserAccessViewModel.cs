using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel
{
    public class ProjectUserAccessViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public List<UserSelectViewModel> UserList { get; set; }

    }
}