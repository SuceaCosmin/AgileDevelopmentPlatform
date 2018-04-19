using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgileDevelopmentPlatform.Models;
using AgileDevelopmentPlatform.ViewModel;

namespace AgileDevelopmentPlatform.Controllers.Api
{
    public class TasksController : ApiController
    {
        private readonly DataManager _manager;

        public TasksController()
        {
            _manager = new DataManager();
        }

        // GET api/<controller>
        public IEnumerable<ReferenceTaskViewModel> Get()
        {
            List<ReferenceTaskViewModel> taskList= new List<ReferenceTaskViewModel>();

            _manager.TaskList.ForEach(model =>
            {
                taskList.Add(new ReferenceTaskViewModel()
                {
                    Id = model.Id,
                    Name = model.Name
                });
            });

            return taskList;
        }

        // GET api/<controller>/5
        public ReferenceTaskViewModel Get(int id)
        {
            var taskInDb = _manager.FindTaskById(id);
            if (taskInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            ReferenceTaskViewModel model=new ReferenceTaskViewModel()
            {
                Id = taskInDb.Id,
                Name = taskInDb.Name
            };


            return model;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var projectInDb = _manager.FindTaskById(id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _manager.RemoveTask(id);
            _manager.SaveChanges();

        }
    }
}