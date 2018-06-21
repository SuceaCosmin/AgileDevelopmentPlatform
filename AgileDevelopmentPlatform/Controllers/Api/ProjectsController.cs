using AgileDevelopmentPlatform.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace AgileDevelopmentPlatform.Controllers.Api
{
    public class ProjectsController : ApiController
    {

        private DataManager dataManager;

        public ProjectsController()
        {
            dataManager = new DataManager();
            
        }

        // GET /api/projects
        public IEnumerable<ProjectModel> GetProjectModels()
        {
            return dataManager.ProjectList;
        }

        //GET /api/projects/1
        public IHttpActionResult GetProjectModel(int projectId)
        {
            var project = dataManager.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
                
            }

                return Ok(Mapper.Map<ProjectModel>(project));
            
        }


        //PUT  /api/customers.1
        [HttpPut]
        public void UpdatePRoject(int id, ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var projectInDb = dataManager.FindProjectById(id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            dataManager.UpdateProject(project);
            dataManager.SaveChanges();

        }

        public void DeleteProject(int id)
        {
            var projectInDb = dataManager.FindProjectById(id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            dataManager.RemoveProject(id);
            dataManager.SaveChanges();
        }

    }
}
