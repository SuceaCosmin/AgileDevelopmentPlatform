using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class SprintStatusReportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TaskStatusReportViewModel> TaskStatusList { get; set; }
    }
}