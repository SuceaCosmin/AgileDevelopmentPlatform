using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using AgileDevelopmentPlatform.Models;
using AutoMapper;

namespace AgileDevelopmentPlatform.Controllers.Api
{
    public class ProjectsController : ApiController
    {

        private AgileDevelopmentDatabaseEntities db;

        public ProjectsController()
        {
            db=new AgileDevelopmentDatabaseEntities();
        }

        // GET /api/projects
        public IEnumerable<ProjectModel> GetProjectModels()
        {
            List<ProjectModel> projects= new List<ProjectModel>();

            foreach (var project in db.Projects.ToList())
            {
                projects.Add(Mapper.Map<ProjectModel>(project));
            }

            return projects;
        }

        //GET /api/projects/1
        public IHttpActionResult GetProjectModel(int projectId)
        {
           var project= db.Projects.SingleOrDefault(proj => proj.ID == projectId);
            if (project == null)
            {
                return NotFound();
                
            }

                return Ok(Mapper.Map<ProjectModel>(project));
            
        }

        //POST /api/projects
        [HttpPost]
        public IHttpActionResult CreateProjectModel(ProjectModel newProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Project project = Mapper.Map<Project>(newProject);
            db.Projects.Add(project);
            db.SaveChanges();
            Mapper.Map(project, newProject);
            return Created(new Uri(Request.RequestUri+"/"+newProject.Id), newProject);

        }

        //PUT  /api/customers.1
        [HttpPut]
        public void UpdatePRoject(int id, ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var projectInDb = db.Projects.SingleOrDefault(proj => proj.ID == id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(project, projectInDb);
            db.SaveChanges();

        }

        public void DeleteProject(int id)
        {
            var projectInDb = db.Projects.SingleOrDefault(proj => proj.ID == id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.Projects.Remove(projectInDb);
            db.SaveChanges();
        }

    }
}
