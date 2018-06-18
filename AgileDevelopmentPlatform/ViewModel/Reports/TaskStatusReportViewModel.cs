using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class TaskStatusReportViewModel
    {
        public string Name { get; set; }

        public string ResponsibleUser { get; set; }

        public string Dificulty { get; set; }

        public int EffortEstimation { get; set; }

        public int WorkEffort { get; set; }

        public string Status { get; set; }

        public bool EstimationExceded => WorkEffort > EffortEstimation;
    }
}