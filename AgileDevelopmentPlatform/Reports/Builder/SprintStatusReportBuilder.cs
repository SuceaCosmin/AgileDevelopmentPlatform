using System;
using System.Collections.Generic;
using System.Linq;
using AgileDevelopmentPlatform.Models;

namespace AgileDevelopmentPlatform.Reports.Builder
{
    public class SprintStatusReportBuilder
    {

        private readonly DataManager _dataManager;

        public SprintStatusReportBuilder(DataManager manager)
        {
            _dataManager = manager;
        }

        public SprintStatusModel BuildSprintStatus(int sprintId)
        {
           SprintModel sprint= _dataManager.FindSprintById(sprintId);
   
            var taskDificultyLevels = _dataManager.TaskDificultyLevels;
            var userList = _dataManager.UserList;
            var tasklist = _dataManager.TaskList.Where(task => task.SprintId == sprint.Id).ToList();
            int overallEffortEstimation = 0;
            int overallWorkEffort = 0;
            List<TaskStatusModel> taskStatusModelList = new List<TaskStatusModel>();
            tasklist.ForEach(task =>
            {
                try
                {
                    TaskStatusModel model = BuildTaskStatus(task, taskDificultyLevels, userList);
                    overallEffortEstimation += model.EffortEstimation;
                    overallWorkEffort += model.WorkEffort;
                    taskStatusModelList.Add(model);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to calculate task status for task {0} {1} due to :{2}", task.Id, task.Name,e.Message);
                }

            });
            int totalNumberOfTasks = taskStatusModelList.Count;
            int numberOfFinishedTasks = 0;

            taskStatusModelList.ForEach(task =>
            {
                if (task.Status.Equals(TaskState.Finished))
                {
                    numberOfFinishedTasks++;
                }
            });



            int percentageOfCompletion=0;
            try
            {
                percentageOfCompletion = numberOfFinishedTasks * 100 / totalNumberOfTasks;
            }
            catch (Exception)
            {
                //Devided by zero exception
            }

     



            SprintStatusModel sprintStatusModel= new SprintStatusModel()
            {
                Id = sprint.Id,
                Name = sprint.Name,
                TargetDate = sprint.TargetDate,
                PercentageOfCompletion = percentageOfCompletion,
                OverallEffortEstimation = overallEffortEstimation,
                OverallWorkEffort = overallWorkEffort,
                TaskStatusList = taskStatusModelList
            };

            return sprintStatusModel;
        }

        public TaskStatusModel BuildTaskStatus(TaskModel model,List<TaskDificulty> dificultyLevels,List<UserModel>userList)
        {
            string taskDificulty = dificultyLevels.FirstOrDefault(dificulty => dificulty.Id == model.TaskDificultyId).Name;
            string userName = userList.FirstOrDefault(user => user.Id.Equals(model.ResponsibleUserId)).UserName;

            return new TaskStatusModel()
            {
                Name = model.Name,
                Dificulty = taskDificulty,
                EffortEstimation = model.EffortEstimation,
                WorkEffort = model.WorkEffort,
                Status = model.Status,
                ResponsibleUser = userName
                
            };
        }
    }
}