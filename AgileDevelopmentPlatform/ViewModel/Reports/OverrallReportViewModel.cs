using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class OverrallReportViewModel
    {
        public TopContributorsReportViewModel TopContributorsReportViewModel { get; set; }
        public List<SprintStatusReportViewModel> SprintStatusReportViewList { get; set; }
    }
}