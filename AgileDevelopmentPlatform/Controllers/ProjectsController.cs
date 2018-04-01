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

        public ActionResult ViewProject(int id)
        {
            var project = dataManager.FindProjectById(id);

             
            if (project == null)
            {
                return HttpNotFound();
            }
            List<ReferenceTaskViewModel> taskList= new List<ReferenceTaskViewModel>();
            foreach (var taskModel in project.TaskList)
            {
                ReferenceTaskViewModel taskReference = new ReferenceTaskViewModel()
                {
                    TaskId = taskModel.Id,
                    TaskName = taskModel.Name
                };
                taskList.Add(taskReference);
            }

            ProjectViewModel viewModel = new ProjectViewModel()
            {
                Name = project.Name,
                Id = project.Id,
                TaskList = taskList
            };


            return View("ViewProject", viewModel);
          
        }

        public ActionResult NewTask(int projectID)
        {


            ViewBag.PriorityList = TaskPriority.List;
            NewTaskViewModel viewModel= new NewTaskViewModel()
            {
                ProjectId =projectID,
                PriorityType = TaskPriority.List
              
                
            };
            return PartialView("NewTask", viewModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddNewTask(NewTaskViewModel taskView)
        {
            //TODO: see how to make the client side validation work.
            //TODO: there is a a null pointer exception on DropDownList in case of sending view again due to invalid data
            if (!ModelState.IsValid)
            {
                return PartialView("NewTask", taskView);
            }

            //TODO update the behavior so that the responconsible user can be null at creation time and the Initiator shall be the logged user
            TaskModel model=new TaskModel()
            {
                Id=0,
                Name = taskView.Name,
                TaskInitiatorId = 1,
                ResponsibleUserId = 1,
                Description = taskView.Description,
                ProjectId = taskView.ProjectId,
                Status = "Open"
            };

            dataManager.AddTask(model);
            dataManager.SaveChanges();

            return RedirectToAction("ViewProject", "Projects",new{Id=taskView.ProjectId});
        }

        [ValidateAntiForgeryToken]
        public ActionResult UpdateTask(EditTaskViewModel taskView)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("EditTask", taskView);
            }

            TaskModel taskModel=Mapper.Map<TaskModel>(taskView);
            dataManager.UpdateTask(taskModel);
            dataManager.SaveChanges();


            return RedirectToAction("ViewProject", "Projects", new { Id = 8 });
        }

        public ActionResult EditTask(int taskId)
        {
           
            TaskModel task= dataManager.FindTaskById(taskId);

            if (task == null)
            {
                return HttpNotFound();
            }

            List<UserSelectViewModel> userList =new List<UserSelectViewModel>();
            foreach (var userModel in dataManager.UserList)
            {
                UserSelectViewModel user=new UserSelectViewModel()
                {
                    UserId = userModel.Id,
                    UserName = userModel.UserName
                };
                userList.Add(user);
                
            }


            EditTaskViewModel model = Mapper.Map<EditTaskViewModel>(task);
            model.UserList = userList;
            model.PriorityType = TaskPriority.List;
            model.TaskStateList = TaskState.List;

            return  PartialView("EditTask", model);
        }
    }
}