using AgileDevelopmentPlatform.Models;
using AgileDevelopmentPlatform.ViewModel;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AgileDevelopmentPlatform.Reports.Builder;
using AgileDevelopmentPlatform.ViewModel.Reports;

namespace AgileDevelopmentPlatform.Controllers
{
    [Authorize]
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
            //TODO: Only the project owner shall have access to  edit or delete the project.
            var userId = HttpContext.User.Identity.GetUserId();
            var userAccessList=  _dataManager.GetUserAccessOnProjects(userId);


            List<ProjectModel> allowedProjects= new List<ProjectModel>();

            _dataManager.ProjectList.ForEach(project =>
            {
                if (project.OwnerId.Equals(userId)||
                    userAccessList.Any(access => access.ProjectId == project.Id))
                {
                    allowedProjects.Add(project);
                }
            });

            return View(allowedProjects);
        }

        #region Project

        public ActionResult NewProject()
        {
            ProjectCreateOrEditViewModel viewModel = new ProjectCreateOrEditViewModel();
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

            ProjectModel projectModel = new ProjectModel()
            {
                Id = project.Id,
                Name = project.Name,
                OwnerId = project.OwnerId

            };

            if (projectModel.Id == 0)
            {
                var userId = HttpContext.User.Identity.GetUserId();
                projectModel.OwnerId = userId;
                _dataManager.AddProject(projectModel);
                _dataManager.SaveChanges();


            }
            else
            {
                _dataManager.UpdateProject(projectModel);
                _dataManager.SaveChanges();
            }


            return RedirectToAction("Index", "Projects");
        }

        public ActionResult EditProject(int id)
        {

            var project = _dataManager.FindProjectById(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var userId = HttpContext.User.Identity.GetUserId();
            var userAccessList = _dataManager.GetUserAccessOnProject(id);
            if (!userAccessList.Any(access => access.UserId.Equals(userId)))
            {
                //TODO: Update to display error that the user does not have access to the project
                return HttpNotFound();
            }

            ProjectCreateOrEditViewModel viewViewModel = new ProjectCreateOrEditViewModel()
            {
                Id = project.Id,
                Name = project.Name
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

            var userId = HttpContext.User.Identity.GetUserId();
            var userAccessList = _dataManager.GetUserAccessOnProject(id);
            if (!userAccessList.Any(access => access.UserId.Equals(userId)))
            {
                //TODO: Update to display error that the user does not have access to the project
                return HttpNotFound();
            }

            List<ReferenceTaskViewModel> taskList = new List<ReferenceTaskViewModel>();
            foreach (var taskModel in project.TaskList)
            {
                if (taskModel.SprintId == null || taskModel.SprintId == 0)
                {

                    ReferenceTaskViewModel taskReference = new ReferenceTaskViewModel()
                    {
                        Id = taskModel.Id,
                        Name = taskModel.Name
                    };
                    if (taskModel.Status.Equals(TaskState.Finished))
                    {
                        taskReference.IsCompleted = true;
                    }

                    taskList.Add(taskReference);
                }

            }

            List<SprintViewModel> sprintList = new List<SprintViewModel>();
            foreach (var sprintModel in project.SprintList)
            {
                SprintViewModel model = Mapper.Map<SprintViewModel>(sprintModel);

                int openTasks = 0;
                int workingTask = 0;
                int finishedTasks = 0;
                int percentageOfCompletion = 0;
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
                                task.IsCompleted = true;
                                finishedTasks++;
                            }
                        }

                    });
                    int totalNumberOfTasks = openTasks + workingTask + finishedTasks;
                    percentageOfCompletion = finishedTasks * 100 / totalNumberOfTasks;

                }
                catch
                {
                    Console.WriteLine("Failed to calculate  sprint task status for project " + project.Name);
                }

                model.OpenTasks = openTasks;
                model.WorkingTasks = workingTask;
                model.FinishedTasks = finishedTasks;
                model.PercentageOfCompletion = percentageOfCompletion;

                sprintList.Add(model);

            }


            ProjectViewModel viewModel = new ProjectViewModel()
            {
                Name = project.Name,
                Id = project.Id,
                OwnerId = project.OwnerId,
                TaskList = taskList,
                SprintList = sprintList

            };


            return View("ViewProject", viewModel);

        }

        public ActionResult EditProjectAccess(int id)
        {
            var project = _dataManager.FindProjectById(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var userAccessList=  _dataManager.GetUserAccessOnProject(id);

            var userList = _dataManager.UserList;

           List<UserSelectViewModel> usersWithAccess= new List<UserSelectViewModel>();

           userAccessList.ForEach(userAccess =>
           {
               var user = userList.Find(model => model.Id.Equals(userAccess.UserId));
               if (user != null)
               {
                   usersWithAccess.Add(Mapper.Map<UserSelectViewModel>(user));
               }
           });

           ProjectAccessViewModel projectAccessViewModel= new ProjectAccessViewModel()
           {
               ProjectName = project.Name,
               ProjectId = project.Id,
               GrantedAccessList = usersWithAccess
           };

            return View("AccessManagement", projectAccessViewModel);
        }

        public ActionResult AddUserToProject(int projectId)
        {

           var currentProject= _dataManager.ProjectList.SingleOrDefault(project => project.Id == projectId);
            if (currentProject == null)
            {
                return HttpNotFound();
            }

           var userAccess= _dataManager.GetUserAccessOnProject(projectId);

            List<UserSelectViewModel> selectableUsers = new List<UserSelectViewModel>
            {
                new UserSelectViewModel()
                {
                    Id = "",
                    UserName = "<none>"
                }
            };

            _dataManager.UserList.ForEach(user =>
            {
                if (userAccess.Find(access => access.UserId.Equals(user.Id)) == null)
                {
                    selectableUsers.Add(Mapper.Map<UserSelectViewModel>(user));
                }
            });
            ProjectUserAccessViewModel model =new ProjectUserAccessViewModel()
            {
                Id = 0,
                ProjectId = projectId,
                UserList = selectableUsers
            };
            return PartialView("AddUserToProject", model);
        }

        public ActionResult GrantUserAccessToProject(ProjectUserAccessViewModel model)
        {

            if (model.UserId!=null && !model.UserId.Equals(""))
            {
                UserAccessModel userAccessModel = new UserAccessModel()
                {
                    Id = model.Id,
                    ProjectId = model.ProjectId,
                    UserId = model.UserId
                };
                _dataManager.AddUserAccessToProject(userAccessModel);
                _dataManager.SaveChanges();
            }

            return RedirectToAction("EditProjectAccess", "Projects", new { Id = model.ProjectId });

        }

        #endregion

        #region Sprint

        public ActionResult NewSprint(int projectId)
        {
            NewSprintViewModel model = new NewSprintViewModel()
            {
                ProjectId = projectId
            };
            return PartialView("NewSprint", model);
        }

        public ActionResult SaveSprint(NewSprintViewModel sprintViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("NewSprint", sprintViewModel);
            }

            SprintModel sprintModel = Mapper.Map<SprintModel>(sprintViewModel);

            _dataManager.AddSprint(sprintModel);
            _dataManager.SaveChanges();

            return RedirectToAction("ViewProject", "Projects", new { Id = sprintViewModel.ProjectId });
        }

        public ActionResult AssignTaskToSprint(int taskId)
        {
            var task = _dataManager.FindTaskById(taskId);

            if (task == null)
            {
                return HttpNotFound();
            }

            var project = _dataManager.FindProjectById(task.ProjectId);
            if (project == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> sprintList = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "",
                    Value = "0"
                }
            };
            project.SprintList.ForEach(sprint =>
            {
                sprintList.Add(new SelectListItem()
                {
                    Text = sprint.Name,
                    Value = sprint.Id.ToString()
                });
            });

            AssignTaskToSprintViewModel model = new AssignTaskToSprintViewModel()
            {
                TaskId = taskId,
                SprintList = sprintList
            };

            return PartialView("AssignTaskToSprint", model);
        }

        public ActionResult SaveTaskAssignation(AssignTaskToSprintViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //TODO read the sprints
                return PartialView("AssignTaskToSprint", model);
            }


            var task = _dataManager.FindTaskById(model.TaskId);

            if (model.SprintId == 0)
            {
                return RedirectToAction("ViewProject", "Projects", new { Id = task.ProjectId });
            }


            task.SprintId = model.SprintId;
            _dataManager.UpdateTask(task);
            _dataManager.SaveChanges();


            return RedirectToAction("ViewProject", "Projects", new { Id = task.ProjectId });
        }

        #endregion

        #region Task


        public ActionResult NewTask(int projectId)
        {

             List<TaskDificulty> dificultyLevels = _dataManager.TaskDificultyLevels;
             List<TaskDificultyViewModel> taskDificultyViewModelList= new List<TaskDificultyViewModel>();

            foreach (var taskDificultyLevel in dificultyLevels)
            {
                TaskDificultyViewModel taskDificultyViewModel= Mapper.Map<TaskDificultyViewModel>(taskDificultyLevel);
                taskDificultyViewModelList.Add(taskDificultyViewModel);
            }



            NewTaskViewModel viewModel = new NewTaskViewModel()
            {
                ProjectId = projectId,
                PriorityType = TaskPriority.List,
                TaskDificultyList = taskDificultyViewModelList,
      
            };
            return PartialView("NewTask", viewModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddNewTask(NewTaskViewModel taskView)
        {

            //TODO: Effort estimation is not saved

            //TODO: see how to make the client side validation work.
            //TODO: there is a a null pointer exception on DropDownList in case of sending view again due to invalid data


            if (!ModelState.IsValid)
            {
                return PartialView("NewTask", taskView);
            }

        
            //TODO update the behavior so that the responconsible user can be null at creation time and the Initiator shall be the logged user
            var userId = HttpContext.User.Identity.GetUserId();

            var userModel = _dataManager.FindUserByUserId(userId);
            TaskModel model = new TaskModel()
            {
                Id = 0,
                Name = taskView.Name,
                TaskInitiatorId = userModel.Id,
                ResponsibleUserId = userModel.Id,
                Description = taskView.Description,
                ProjectId = taskView.ProjectId,
                Priority = taskView.Priority,
                EffortEstimation = taskView.EffortEstimation,
                WorkEffort = 0,
                TaskDificultyId = taskView.TaskDificulty,
                TaskCreationDate = DateTime.Today,
                TaskCompletionDate = null,
                Status = TaskState.Open
            };

            _dataManager.AddTask(model);
            _dataManager.SaveChanges();

            return RedirectToAction("ViewProject", "Projects", new { Id = taskView.ProjectId });
        }

        [ValidateAntiForgeryToken]
        public ActionResult UpdateTask(EditTaskViewModel taskView)
        {
            if (!ModelState.IsValid)
            {
                List<UserSelectViewModel> userList = new List<UserSelectViewModel>();
                TaskModel model = _dataManager.FindTaskById(taskView.Id);

                var usersOnProject = _dataManager.GetProjectTeam(model.ProjectId);

                foreach (var userModel in usersOnProject)
                {

                    var selectUser = Mapper.Map<UserSelectViewModel>(userModel);
                    userList.Add(selectUser);
                }
                taskView.UserList = userList;
                taskView.PriorityType = TaskPriority.List;
                taskView.TaskStateList = TaskState.List;

                return PartialView("EditTask", taskView);
            }

            TaskModel taskModel = Mapper.Map<TaskModel>(taskView);
            if (taskModel.Status.Equals(TaskState.Finished))
            {
                taskModel.TaskCompletionDate= DateTime.Today;
            }

            _dataManager.UpdateTask(taskModel);
            _dataManager.SaveChanges();
     
            return RedirectToAction("ViewProject", "Projects", new { Id = taskModel.ProjectId });
        }

        public ActionResult EditTask(int taskId)
        {

            TaskModel task = _dataManager.FindTaskById(taskId);

            if (task == null)
            {
                return HttpNotFound();
            }


            //TODO: Return only the users that have access to the project.
            List<UserSelectViewModel> userList = new List<UserSelectViewModel>();
            foreach (var userModel in _dataManager.GetProjectTeam(task.ProjectId))
            {

                var selectUser = Mapper.Map<UserSelectViewModel>(userModel);
                userList.Add(selectUser);
            }
            List<TaskDificulty> dificultyLevels = _dataManager.TaskDificultyLevels;
            List<TaskDificultyViewModel> taskDificultyViewModelList = new List<TaskDificultyViewModel>();

            foreach (var taskDificultyLevel in dificultyLevels)
            {
                TaskDificultyViewModel taskDificultyViewModel = Mapper.Map<TaskDificultyViewModel>(taskDificultyLevel);
                taskDificultyViewModelList.Add(taskDificultyViewModel);
            }

            EditTaskViewModel model = Mapper.Map<EditTaskViewModel>(task);
            model.UserList = userList;
          
            model.PriorityType = TaskPriority.List;
            model.TaskStateList = TaskState.List;
            model.TaskDificultyList = taskDificultyViewModelList;

            return PartialView("EditTask", model);
        }

        public ActionResult ReopenTask(int id)
        {
           var task= _dataManager.FindTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            task.Status = TaskState.Working;
            _dataManager.UpdateTask(task);
            _dataManager.SaveChanges();
            return RedirectToAction("ViewProject", "Projects", new { Id = task.ProjectId });
        
           
        }
        #endregion

        #region Report

        public ActionResult Reports(int projectId)
        {
            var project = _dataManager.FindProjectById(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }
           
            TopContributorsReportBuilder builder = new TopContributorsReportBuilder(_dataManager);

            List<UserContribution> contributionList= builder.CalculateUserContributions(project);

            List<UserContributionViewModel> viewModels= new List<UserContributionViewModel>();
            contributionList.ForEach(contribution => viewModels.Add(Mapper.Map<UserContributionViewModel>(contribution)) );

            TopContributorsReportViewModel topContributorsReportViewModel= new TopContributorsReportViewModel()
            {
                ProjectName = project.Name,
                ProjectId =project.Id,
                UserContributionList = viewModels
            };

           
            List<SprintStatusModel> sprintStatusModelList = new List<SprintStatusModel>();

            SprintStatusReportBuilder statusReportBuilder= new SprintStatusReportBuilder(_dataManager);

            project.SprintList.ForEach(sprint =>
            {
                SprintStatusModel sprintStatusModel = statusReportBuilder.BuildSprintStatus(sprint.Id);
                sprintStatusModelList.Add(sprintStatusModel);
            });


            List<SprintStatusReportViewModel> sprintStatusReportViewModelList =
                Mapper.Map<List<SprintStatusReportViewModel>>(sprintStatusModelList);

            OverrallReportViewModel model= new OverrallReportViewModel()
            {
                TopContributorsReportViewModel = topContributorsReportViewModel,
                SprintStatusReportViewList = sprintStatusReportViewModelList
            };

            return View("Reports/Reports", model);
        }

        public ActionResult PrintTopContributorsReport(int projectId)
        {
            var project = _dataManager.FindProjectById(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }

            TopContributorsReportBuilder builder = new TopContributorsReportBuilder(_dataManager);

            List<UserContribution> contributionList = builder.CalculateUserContributions(project);

            List<UserContributionViewModel> viewModels = new List<UserContributionViewModel>();
            contributionList.ForEach(contribution => viewModels.Add(Mapper.Map<UserContributionViewModel>(contribution)));

            TopContributorsReportViewModel model = new TopContributorsReportViewModel()
            {
                ProjectName = project.Name,
                ProjectId = project.Id,
                UserContributionList = viewModels
            };

            var action= new Rotativa.ViewAsPdf("Reports/_partialTopContributorReport", model: model)
             {
                 FileName = "Project Summary.pdf"
             };
            return action;
        }

        public ActionResult PrintSprintStatusReport(int sprintId)
        {
            SprintModel sprintModel = _dataManager.FindSprintById(sprintId);

            if (sprintModel == null)
            {
                HttpNotFound();
            }

            SprintStatusReportBuilder statusReportBuilder = new SprintStatusReportBuilder(_dataManager);
            var sprintReport = statusReportBuilder.BuildSprintStatus(sprintModel.Id);

            SprintStatusReportViewModel model = Mapper.Map<SprintStatusReportViewModel>(sprintReport);

            var action = new Rotativa.ViewAsPdf("Reports/_partialSprintStatusReport", model: model)
            {
                FileName = "Sprint status report.pdf"
            };
            return action;

        }


        #endregion

    }
}