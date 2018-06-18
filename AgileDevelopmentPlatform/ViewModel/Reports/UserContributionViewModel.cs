using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.ViewModel.Reports
{
    public class UserContributionViewModel
    {
        public String Name { get; set; }
        public int ContributionPoints { get; set; }
        public double ContributionPercentage { get; set; }
    }
}