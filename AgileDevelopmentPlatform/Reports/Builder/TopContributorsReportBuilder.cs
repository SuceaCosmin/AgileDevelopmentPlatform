using System;
using System.Collections.Generic;
using System.Linq;
using AgileDevelopmentPlatform.Models;

namespace AgileDevelopmentPlatform.Reports.Builder
{
    public class TopContributorsReportBuilder
    {
        private DataManager _dataManager;

        public TopContributorsReportBuilder(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<UserContribution> CalculateUserContributions(ProjectModel project)
        {
            List<UserContribution> userContributionList = new List<UserContribution>();

            var userAccessList = _dataManager.GetUserAccessOnProject(project.Id);
            var userList = _dataManager.UserList;
            var taskList = _dataManager.FindTasksByProjectId(project.Id);
            var taskDificultyLevels = _dataManager.TaskDificultyLevels;
            List<UserModel> allowedUsers = new List<UserModel>();
            userAccessList.ForEach(userAccess =>
            {
                var result = userList.Find(user => user.Id.Equals(userAccess.UserId));
                allowedUsers.Add(result);
            });

            int totalContributionPoints = 0;

            foreach (var taskModel in taskList)
            {
                if (taskModel.Status.Equals(TaskState.Finished))
                {
                    var taskDificulty = taskDificultyLevels.Find(dificultyLevel => dificultyLevel.Id==taskModel.TaskDificultyId);
                    totalContributionPoints += taskDificulty.ContributionPoints;
                }
            }
            foreach (var user in allowedUsers)
            {
                List<TaskModel> userTaskList = taskList.FindAll(task => task.ResponsibleUserId.Equals(user.Id)
                                                                        && task.Status.Equals(TaskState.Finished));
                int contributionPoints = 0;
                foreach (var task in userTaskList)
                {
                    var taskDificulty = taskDificultyLevels.Find(t => t.Id.Equals(task.TaskDificultyId));
                    if (taskDificulty != null)
                    {
                        contributionPoints += taskDificulty.ContributionPoints;
                    }
                }

                double contributionPercentage = 0;
                try
                {
                    contributionPercentage=contributionPoints * 100 / totalContributionPoints ;
                }
                catch (DivideByZeroException )
                {
                    //TODO: Add message maybe
                }

               

                var contributionViewModel = new UserContribution()
                {
                    Name = user.UserName,
                    ContributionPoints = contributionPoints,
                    ContributionPercentage = contributionPercentage
                };
                userContributionList.Add(contributionViewModel);
            }

            return userContributionList.OrderByDescending(contribution => totalContributionPoints).ToList();
                     

        }
    }
}