using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace cms.ViewModels
{
    public class ToDoFlyOutViewModel : ViewModelBase
    {
        public ToDo ToDo { get; set; }
        
        public ToDoFlyOutViewModel(ToDo toDo)
        {            
            this.ToDo = toDo;            
        }        
    }
}
