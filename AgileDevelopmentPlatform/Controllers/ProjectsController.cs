using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AgileDevelopmentPlatform.Models;
using AutoMapper;

namespace AgileDevelopmentPlatform.Controllers
{
    public class ProjectsController : Controller
    {

        private AgileDevelopmentDatabaseEntities entities;
        private Projects ProjectList;

        public ProjectsController()
        {
            entities = new AgileDevelopmentDatabaseEntities();
            ProjectList=new Projects();
            foreach (Project project in entities.Projects.ToList())
            {
                ProjectList.Add(Mapper.Map<ProjectModel>(project));
            }


        }

        // GET: Projects
        public ActionResult Index()
        {
             
            return View(ProjectList);
        }

        public ActionResult New()
        {
            return View("ProjectForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return View("ProjectForm",project);
            }

            if (project.Id == 0)
            {
                Project newProject = Mapper.Map<Project>(project);
                entities.Projects.Add(newProject);
                entities.SaveChanges();
            }
            else
            {
                var projectInDb=entities.Projects.Single(proj => proj.ID == project.Id);
                Mapper.Map(project, projectInDb);
                entities.SaveChanges();
            }
   

            return RedirectToAction("Index", "Projects");
        }


        public ActionResult Edit(int id)
        {
            var project = ProjectList.List.Find(proj => proj.Id == id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View("ProjectForm",project);
           
        }
    }
}