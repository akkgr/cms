using cms.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cms.Models
{
    public class ToDo : ViewModelBase
    {
        public ToDo()
        {
            Validator = new ToDoValidator();
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string PersonId { get; set; }
        public DateTime ToDoDate { get; set; }
        public bool Done { get; set; }
        public virtual Person Person { get; set; }

        public string SearchText
        {
            get
            {
                return string.Format("{0} {1}", Description, Person == null ? string.Empty : Person.FullName);
            }
        }
    }
}
