using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AgileDevelopmentPlatform.Models;

namespace AgileDevelopmentPlatform.Controllers.Api
{
    public class SprintsController : ApiController
    {
        private readonly DataManager _dataManager = new DataManager();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var sprint = _dataManager.FindSprintById(id);
            if (sprint == null)
            {
                return NotFound();
            }
            return Ok(sprint);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
           throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
           throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var sprint = _dataManager.FindSprintById(id);
            if (sprint != null)
            {
                _dataManager.RemoveSprint(id);
                _dataManager.SaveChanges();
            }
        }
    }
}