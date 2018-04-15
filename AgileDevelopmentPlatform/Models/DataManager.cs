using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace AgileDevelopmentPlatform.Models
{
    public class DataManager
    {
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
                    projectList.Add(Mapper.Map<ProjectModel>(project));
                }

                return projectList;
            }
        }

        public ProjectModel AddProject(ProjectModel newProject)
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
            Mapper.Map(project, projectInDb);
        }

        public bool RemoveProject(int id)
        {
            Project deleteProject = _databaseEntities.Projects.SingleOrDefault(project=>project.Id==id);
           
            if (deleteProject != null)
            {
                _databaseEntities.Projects.Remove(deleteProject);
                return true;
            }

            return false;
        }
        #endregion

        #region Sprint region

        public List<SprintModel> FindSprintByProjectName(int projectId)
        {
            List<SprintModel> sprintList=new List<SprintModel>();

            return sprintList;
        }

        #endregion

        #region Task region

        public List<TaskModel> TaskList {
            get
            {
                List<TaskModel> taskList= new List<TaskModel>();
                foreach (var databaseEntitiesTask in _databaseEntities.Tasks)
                {
                    taskList.Add(Mapper.Map<TaskModel>(databaseEntitiesTask));
                }

                return taskList;
            }
        }

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
               
                if (taskModel.SprintId == null || taskModel.SprintId == 0)
                {
                    taskModel.SprintId = taskInDb.SprintId;
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

        #endregion

        #region User region

        public void AddUser(UserModel userModel)
        {
            User user = Mapper.Map<User>(userModel);
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

        public UserModel FindUserByUserName(string userName)
        {
            var sprint = _databaseEntities.Users.SingleOrDefault(user=>user.Name.Equals(userName));
            if (sprint != null)
            {
                return Mapper.Map<UserModel>(sprint);
            }

            return null;         
        }

        #endregion

        public void SaveChanges()
        {
            _databaseEntities.SaveChanges();
        }


        public void AddSprint(SprintModel sprintModel)
        {
            Sprint sprint = Mapper.Map<Sprint>(sprintModel);
            _databaseEntities.Sprints.Add(sprint);
        }
    }
}