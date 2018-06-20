using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class TopContributorsReportViewModel
    {
   
        public int ProjectId { get; set; }

        public String ProjectName { get; set; }

        public List<UserContributionViewModel> UserContributionList { get; set; }

        public bool HideGenerate { get; set; }


    }
}