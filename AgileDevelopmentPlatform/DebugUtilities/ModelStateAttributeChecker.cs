using System.Diagnostics;
using System.Web.Mvc;

namespace AgileDevelopmentPlatform.DebugUtilities
{
    public class ModelStateAttributeChecker
    {
        public static void DisplayModelAttributeState(ModelStateDictionary modelstate)
        {
            foreach (var modelStateKey in modelstate.Keys)
            {
                Debug.WriteLine("{0} is valid:{1}", modelStateKey, modelstate.IsValidField(modelStateKey));
            }
        }
      
    }
}