using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace AgileDevelopmentPlatform.Models
{
    public class DataManager
    {
        private AgileDevelopmentDatabaseEntities databaseEntities;


        public DataManager()
        {
            databaseEntities=new AgileDevelopmentDatabaseEntities();
        }

        public List<ProjectModel> ProjectList
        {
            get
            {
                List<ProjectModel> projectList = new List<ProjectModel>();
                foreach (var project in databaseEntities.Projects)
                {
                    projectList.Add(Mapper.Map<ProjectModel>(project));
                }

                return projectList;
            }        
        }

        public ProjectModel FindProjectById(int id)
        {
            foreach (Project project in databaseEntities.Projects)
            {
                if (project.Id == id)
                {
                    return Mapper.Map<ProjectModel>(project);
                }
            }

            return null;
        }

        public ProjectModel AddProject(ProjectModel newProject)
        {
            Project project = Mapper.Map<Project>(newProject);
            databaseEntities.Projects.Add(project);
            SaveChanges();
            return Mapper.Map<ProjectModel>(project);
        }

        public void SaveChanges()
        {
            databaseEntities.SaveChanges();
        }

        public void UpdateProject(ProjectModel project)
        {
            var projectInDb = databaseEntities.Projects.Single(proj => proj.Id == project.Id);
            Mapper.Map(project, projectInDb);
        }

        public bool RemoveProject(int id)
        {
            Project deleteProject = null;
            foreach (Project project in databaseEntities.Projects)
            {
                if (project.Id == id)
                {
                    deleteProject = project;
                }
            }

            if (deleteProject != null)
            {
                databaseEntities.Projects.Remove(deleteProject);
                return true;
            }

            return false;
        }

        public void AddTask(TaskModel taskModel)
        {
            Task task = Mapper.Map<Task>(taskModel);
            databaseEntities.Tasks.Add(task);
        }
    }
}