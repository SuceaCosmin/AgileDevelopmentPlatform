using System.Collections.Generic;

namespace AgileDevelopmentPlatform.Models
{
    public class SprintModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

        public List<TaskModel> Tasks { get; set; }

    }

    public class SprintStatusModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TargetDate { get; set; }

        public int PercentageOfCompletion { get; set; }

        public int OverallEffortEstimation { get; set; }

        public int OverallWorkEffort { get; set; }

        public List<TaskStatusModel> TaskStatusList { get; set; }

    }


}