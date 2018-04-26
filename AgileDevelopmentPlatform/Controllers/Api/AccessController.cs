using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgileDevelopmentPlatform.Models;

namespace AgileDevelopmentPlatform.Controllers.Api
{
    public class AccessController : ApiController
    {
        private readonly DataManager _manager;

        public AccessController()
        {
            _manager = new DataManager();
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
           throw new NotImplementedException();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            throw new NotImplementedException();
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
        [HttpDelete]
        public void Delete(string userId,int projectId)
        {
          bool response=  _manager.RemoveUserAccessOnProject(userId,projectId);
            if(response) _manager.SaveChanges();
        }
    }
}