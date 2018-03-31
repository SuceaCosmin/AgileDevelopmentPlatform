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

        private DataManager dataManager;
 

        public ProjectsController()
        {
            dataManager=new DataManager();
         
        }

        // GET: Projects
        public ActionResult Index()
        {
             
            return View(dataManager.ProjectList);
        }

        public ActionResult New()
        {
            ProjectCreateOrEditViewModel viewModel =new ProjectCreateOrEditViewModel();
            return View("AddProject", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProjectCreateOrEditViewModel project)
        {
            if (!ModelState.IsValid)
            {
                return View("AddProject", project);
            }

            ProjectModel projectModel=new ProjectModel()
            {
                Id = project.Id,
                Name =project.Name
            };

            if (projectModel.Id == 0)
            {
                dataManager.AddProject(projectModel);
                dataManager.SaveChanges();


            }else{
                dataManager.UpdateProject(projectModel);
                dataManager.SaveChanges();
            }
   

            return RedirectToAction("Index", "Projects");
        }

        public ActionResult Edit(int id)
        {
             ;
            var project = dataManager.FindProjectById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ProjectCreateOrEditViewModel viewViewModel = new ProjectCreateOrEditViewModel()
            {
                Id = project.Id,
                Name= project.Name
            };

            return View("AddProject", viewViewModel);
           
        }

        public ActionResult View(int id)
        {
            var project = dataManager.FindProjectById(id);

             
            if (project == null)
            {
                return HttpNotFound();
            }

            ProjectViewModel viewModel = new ProjectViewModel()
            {
                Name = project.Name,
                Id = project.Id,
                TaskList = project.TaskList
            };


            return View("ViewProject", viewModel);
          
        }

        public ActionResult NewTask(int projectID)
        {


            ViewBag.PriorityList = TaskPriorities.PriorityList;
            NewTaskViewModel viewModel= new NewTaskViewModel()
            {
                ProjectID =projectID
                
            };
            return PartialView("NewTask", viewModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult SaveTask(NewTaskViewModel taskView)
        {
            //TODO  see how to make the client side validation work.
            if (!ModelState.IsValid)
            {
                return View("NewTask", taskView);
            }

            TaskModel model=new TaskModel()
            {
                Id=0,
                Name = taskView.Name,
                Description = taskView.Description,
                ProjectId = taskView.ProjectID,
                Status = "Open"
            };

            dataManager.AddTask(model);
            dataManager.SaveChanges();

            return RedirectToAction("View", "Projects",new{Id=taskView.ProjectID});
        }
    }
}