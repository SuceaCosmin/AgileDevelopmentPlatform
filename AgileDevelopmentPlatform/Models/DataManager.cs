﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AgileDevelopmentPlatform.Models
{
    public class DataManager 
    {

        private  readonly int OBJECT_STATE_DELETED =1;
        private readonly AgileDevelopmentDatabaseEntities _databaseEntities;

        public DataManager()
        {
            _databaseEntities=new AgileDevelopmentDatabaseEntities();
        }

        #region Project region

        public List<ProjectModel> ProjectList
        {
            get
            {
                List<ProjectModel> projectList = new List<ProjectModel>();
                foreach (var project in _databaseEntities.Projects)
                {
                    if (project.IsDeleted == 0)
                    {
                        projectList.Add(Mapper.Map<ProjectModel>(project));
                    }
                }

                return projectList;
            }
        }

        public ProjectModel CreateNewProject(ProjectModel newProject)
        {
            Project project = Mapper.Map<Project>(newProject);

            _databaseEntities.Projects.Add(project);

            SaveChanges();
            return Mapper.Map<ProjectModel>(project);
        }

        public ProjectModel FindProjectById(int id)
        {
            foreach (Project project in _databaseEntities.Projects)
            {
                if (project.Id == id)
                {
                    return Mapper.Map<ProjectModel>(project);
                }
            }

            return null;
        }

        public void UpdateProject(ProjectModel project)
        {
            var projectInDb = _databaseEntities.Projects.Single(proj => proj.Id == project.Id);

            projectInDb.ProjectName = project.Name;


        }

        public bool RemoveProject(int id)
        {
            Project deleteProject = _databaseEntities.Projects.SingleOrDefault(project=>project.Id==id);
           
            if (deleteProject != null)
            {
                deleteProject.IsDeleted = OBJECT_STATE_DELETED;
                return true;
            }

            return false;
        }
        #endregion

        #region Sprint region

        public void AddSprint(SprintModel sprintModel)
        {
            Sprint sprint = Mapper.Map<Sprint>(sprintModel);
            _databaseEntities.Sprints.Add(sprint);
        }

        public void UpdateSprint(SprintModel model)
        {
            var sprint=_databaseEntities.Sprints.FirstOrDefault(s => s.Id == model.Id);
            sprint.Name = model.Name;
            sprint.TargetDate = model.TargetDate;
        }

        public SprintModel FindSprintById(int sprintId)
        {
           var model= _databaseEntities.Sprints.FirstOrDefault(sprint => sprint.Id == sprintId);

           return Mapper.Map<SprintModel>(model);

        }


        public void RemoveSprint(int sprintId)
        {

           var currentSprint= _databaseEntities.Sprints.FirstOrDefault(sprint => sprint.Id == sprintId);
            if (currentSprint != null)
            {
                _databaseEntities.Sprints.Remove(currentSprint);
            }

        }

        #endregion

        #region Task region

        public void AddTask(TaskModel taskModel)
        {
            Task task = Mapper.Map<Task>(taskModel);
            _databaseEntities.Tasks.Add(task);
        }

        public TaskModel FindTaskById(int taskId)
        {
            var value = _databaseEntities.Tasks.Find(taskId);
            if (value != null)
            {
                return Mapper.Map<TaskModel>(value);
            }

            return null;

        }

        public List<TaskModel> FindTasksByProjectId(int projectId)
        {
            List<TaskModel> taskList = new List<TaskModel>();
            foreach (Task task in _databaseEntities.Tasks.Where(task => task.ProjectId == projectId))
            {
                TaskModel model = Mapper.Map<TaskModel>(task);
                taskList.Add(model);
            }

            return taskList;
        }

        public void UpdateTask(TaskModel taskModel)
        {
            var taskInDb = _databaseEntities.Tasks.SingleOrDefault(task => task.Id == taskModel.Id);
            if (taskInDb != null)
            {
                //TODO see if there is any other way to prevent the replace of data
                if (taskModel.ProjectId == 0)
                {
                    taskModel.ProjectId = taskInDb.ProjectId;
                }
               

                Mapper.Map(taskModel, taskInDb);
            }
        }

        public bool RemoveTask(int id)
        {
            Task removedTask = _databaseEntities.Tasks.SingleOrDefault(task => task.Id == id);

            if (removedTask != null)
            {
                _databaseEntities.Tasks.Remove(removedTask);
                return true;
            }

            return false;
        }

        public List<TaskModel> TaskList
        {
            get
            {
                List<TaskModel> taskList = new List<TaskModel>();
                foreach (var databaseEntitiesTask in _databaseEntities.Tasks)
                {
                    taskList.Add(Mapper.Map<TaskModel>(databaseEntitiesTask));
                }

                return taskList;
            }
        }

        public List<TaskDificulty> TaskDificultyLevels
        {
            get
            {
                List<TaskDificulty> taskDificultyLevelList = new List<TaskDificulty>();
                foreach (var databaseEntitiesTaskDificultyLevel in _databaseEntities.TaskDificultyLevels)
                {
                    TaskDificulty dificultyLevel = Mapper.Map<TaskDificulty>(databaseEntitiesTaskDificultyLevel);
                    taskDificultyLevelList.Add(dificultyLevel);
                }

                return taskDificultyLevelList;
            }
        }
        #endregion

        #region User region

        public void AddUser(UserModel userModel)
        {
            AgileDevelopmentPlatform.User user = Mapper.Map<AgileDevelopmentPlatform.User>(userModel);
            _databaseEntities.Users.Add(user);
            _databaseEntities.SaveChanges();
        }

        public List<UserModel> UserList {
            get
            {
                List<UserModel>userList=new List<UserModel>();
                foreach (var databaseEntitiesUser in _databaseEntities.Users)
                {
                    userList.Add(Mapper.Map<UserModel>(databaseEntitiesUser));
                }

                return userList;
            }
        }

        /// <summary>
        /// Method used to get the list of users that have access to a project.
        /// </summary>
        /// <param name="projectId">represents the project id</param>
        /// <returns></returns>
        public List<UserModel> GetProjectTeam(int projectId)
        {
            List<UserModel> projectUserList = new List<UserModel>();
            var projectUserAccess=GetUserAccessOnProject(projectId);
            UserList.ForEach(user =>
            {
                if (projectUserAccess.Any(model => model.UserId.Equals(user.Id)))
                {
                    projectUserList.Add(user);
                }
            });


            return projectUserList;
        }

        public UserModel FindUserByUserId(string userId)
        {
            var sprint = _databaseEntities.Users.SingleOrDefault(user=>user.Id.Equals(userId));
            if (sprint != null)
            {
                return Mapper.Map<UserModel>(sprint);
            }

            return null;         
        }

        #endregion

        #region UserAccess region

       

        public void AddUserAccessToProject(UserAccessModel model)
        {
            UserAccess access = Mapper.Map<UserAccess>(model);
            _databaseEntities.UserAccesses.Add(access);
        }

        public List<UserAccessModel> GetUserAccessOnProjects(string userId)
        {
            var accessList=_databaseEntities.UserAccesses.Where(access => access.UserId.Equals(userId)).ToList();

            List<UserAccessModel> userAccessList=new List<UserAccessModel>();


                foreach (UserAccess userAccess in accessList)
                {
                    userAccessList.Add(Mapper.Map<UserAccessModel>(userAccess));
                }

            return userAccessList;
            

        }

        public List<UserAccessModel> GetUserAccessOnProject(int projectId)
        {
            List<UserAccessModel> userAccessList = new List<UserAccessModel>();

            var projectAccess = _databaseEntities.UserAccesses.Where(access => access.ProjectId == projectId);
            foreach (UserAccess access in projectAccess)
            {
                userAccessList.Add(Mapper.Map<UserAccessModel>(access));
            }

            return userAccessList;
        }

        public bool RemoveUserAccessOnProject(string userId, int projectId)
        {
           var userAccess= _databaseEntities.UserAccesses.SingleOrDefault(access =>
                access.ProjectId == projectId && access.UserId.Equals(userId));
            if (userAccess != null)
            {
                _databaseEntities.UserAccesses.Remove(userAccess);
                return true;
            }

            return false;
        }


        #endregion

        public void SaveChanges()
        {
            _databaseEntities.SaveChanges();
        }


 
    }
}