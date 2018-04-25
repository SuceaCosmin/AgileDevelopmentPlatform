using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel
{
    public class ProjectAccessViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public List<UserSelectViewModel> GrantedAccessList { get; set; }
    }
}