using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgileDevelopmentPlatform.Tests
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            AgileDevelopmentDatabaseEntities db= new AgileDevelopmentDatabaseEntities();

            foreach (var project in db.Projects.ToList())
            {
                Console.WriteLine(project.ProjectName);
                foreach (var task in project.Tasks)
                {
                    Console.WriteLine(task.Name);
                }
            }
       }
    }
}
