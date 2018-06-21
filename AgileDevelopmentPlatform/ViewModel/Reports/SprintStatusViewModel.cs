using System.Collections.Generic;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class SprintStatusReportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TargetDate { get; set; }

        public int PercentageOfCompletion { get; set; }

        public int OverallEffortEstimation { get; set; }

        public int OverallWorkEffort { get; set; }

        public List<TaskStatusReportViewModel> TaskStatusList { get; set; }

        public bool HideGenerate { get; set; }
    }
}