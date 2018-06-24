using System.Collections.Generic;

namespace AgileDevelopmentPlatform.ViewModel
{
    public class SprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

        public List<ReferenceTaskViewModel> Tasks { get; set; }

        public int OpenTasks { get; set; }

        public int WorkingTasks { get; set; }

        public int FinishedTasks { get; set; }

        public int PercentageOfCompletion { get; set; }
    }

    public class AddEditSprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TargetDate { get; set; }

    }
}