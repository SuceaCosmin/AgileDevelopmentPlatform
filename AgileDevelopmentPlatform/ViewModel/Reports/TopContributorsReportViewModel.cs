using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class TopContributorsReportViewModel
    {
        public String ProjectName { get; set; }
        public List<UserContributionViewModel> UserContributionList { get; set; }
        public int ProjectId { get; set; }
    }
}