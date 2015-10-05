using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace cms.ViewModels
{
    public class JobFlyOutViewModel : ViewModelBase
    {   
        public Job Job { get; set; }

        public JobFlyOutViewModel(Job job)
        {
            this.Job = job;
        }
    }
}
