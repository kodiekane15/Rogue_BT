using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rogue_BT.ViewModels
{
    public class ProjectWizardWM
    {
        [Required]
        public string Name { get; set; }
        
        public string ProjectManagerId { get; set; }

        public ICollection<string> DeveloperIds { get; set; }

        public ICollection<string> SubmitterIds { get; set; }

        public ProjectWizardWM()
        {
            DeveloperIds = new HashSet<string>();
            SubmitterIds = new HashSet<string>();
        }
    }
}