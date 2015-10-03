using cms.Models.Validations;
using System;
using System.Collections.Generic;

namespace cms.Models
{
    public class Job : ViewModelBase
    {
        public Job()
        {
            Validator = new JobValidator();
        }

        public string Id { get; set; }
        public string PersonId { get; set; }
        public string Description { get; set; }
        public DateTime Implemented { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public virtual Person Person { get; set; }

        public string SearchText
        {
            get
            {
                return string.Format("{0} {1}", Description, Person==null?string.Empty:Person.FullName);
            }
        }
    }
}
