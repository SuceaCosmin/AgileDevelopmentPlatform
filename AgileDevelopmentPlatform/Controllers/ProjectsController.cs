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

        private readonly DataManager _dataManager;
 

        public ProjectsController()
        {
            _dataManager=new DataManager();
         
        }

        // GET: Projects
        public ActionResult Index()
        {
             
            return View(_dataManager.ProjectList);
        }

        public ActionResult NewProject()
        {
            ProjectCreateOrEditViewModel viewModel =new ProjectCreateOrEditViewModel();
            return View("AddProject", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProject(ProjectCreateOrEditViewModel project)
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
                _dataManager.AddProject(projectModel);
                _dataManager.SaveChanges();


            }else{
                _dataManager.UpdateProject(projectModel);
                _dataManager.SaveChanges();
            }
   

            return RedirectToAction("Index", "Projects");
        }

        public ActionResult EditProject(int id)
        {
             ;
            var project = _dataManager.FindProjectById(id);
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
            var project = _dataManager.FindProjectById(id);

             
            if (project == null)
            {
                return HttpNotFound();
            }
            List<ReferenceTaskViewModel> taskList= new List<ReferenceTaskViewModel>();
            foreach (var taskModel in project.TaskList)
            {
                if (taskModel.SprintId == null || taskModel.SprintId == 0)
                {

                    ReferenceTaskViewModel taskReference = new ReferenceTaskViewModel()
                    {
                        Id = taskModel.Id,
                        Name = taskModel.Name
                    };
                    taskList.Add(taskReference);
                }

            }

            List<SprintViewModel> sprintList=new List<SprintViewModel>();
            foreach (var sprintModel in project.SprintList)
            {
                SprintViewModel model = Mapper.Map<SprintViewModel>(sprintModel);

                int openTasks = 0;
                int workingTask = 0;
                int finishedTasks = 0;
                try
                {
                    //TODO  see why what is the problem and why the respons is 0 0 0 
                    model.Tasks.ForEach(task =>
                    {
                        TaskModel currentTask = project.TaskList.Find(taskModel => taskModel.Id == task.Id);

                        if (currentTask.Status != null)
                        {
                            if (currentTask.Status.Equals(TaskState.Open))
                            {
                                openTasks++;
                            }
                            else if (currentTask.Status.Equals(TaskState.Working)
                                     || currentTask.Status.Equals(TaskState.Review))
                            {
                                workingTask++;
                            }
                            else if (currentTask.Status.Equals(TaskState.Finished))
                            {
                                finishedTasks++;
                            }
                        }

                    });
                }catch{
                  Console.WriteLine("Failed to calculate  sprint task status for project "+project.Name);
                }

                model.OpenTasks = openTasks;
                model.WorkingTasks = workingTask;
                model.FinishedTasks = finishedTasks;
                
                sprintList.Add(model);

            }


            ProjectViewModel viewModel = new ProjectViewModel()
            {
                Name = project.Name,
                Id = project.Id,
                TaskList = taskList,
                SprintList = sprintList
                
            };


            return View("ViewProject", viewModel);
          
        }

        public ActionResult NewTask(int projectId)
        {


            ViewBag.PriorityList = TaskPriority.List;
            NewTaskViewModel viewModel= new NewTaskViewModel()
            {
                ProjectId =projectId,
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

            _dataManager.AddTask(model);
            _dataManager.SaveChanges();

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
            _dataManager.UpdateTask(taskModel);
            
            _dataManager.SaveChanges();


            //TODO remove hardcoded id and redirect to current project
            return RedirectToAction("ViewProject", "Projects", new { Id = 8 });
        }

        public ActionResult EditTask(int taskId)
        {
           
            TaskModel task= _dataManager.FindTaskById(taskId);

            if (task == null)
            {
                return HttpNotFound();
            }

            List<UserSelectViewModel> userList =new List<UserSelectViewModel>();
            foreach (var userModel in _dataManager.UserList)
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

        public ActionResult NewSprint(int projectId)
        {
            NewSprintViewModel model= new NewSprintViewModel()
            {
                ProjectId =projectId
            };
            return PartialView("NewSprint", model);
        }

        public ActionResult SaveSprint(NewSprintViewModel sprintViewModel)
        {
            if (!ModelState.IsValid){
                return PartialView("NewSprint", sprintViewModel);
            }

            SprintModel sprintModel = Mapper.Map<SprintModel>(sprintViewModel);

            _dataManager.AddSprint(sprintModel);
            _dataManager.SaveChanges();

            return RedirectToAction("ViewProject", "Projects", new { Id = sprintViewModel.ProjectId });
        }

    }
}