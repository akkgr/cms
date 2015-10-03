using System.Collections.Generic;
using cms.Models.Validations;

namespace cms.Models
{
    public class Person : ViewModelBase
    {
        public Person()
        {
            Jobs = new List<Job>();
            ToDos = new List<ToDo>();

            Validator = new PersonValidator();
        }

        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string FullName { get { return string.Format("{0} {1}",Lastname,Firstname); } }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string OtherPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }

        public string SearchText
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",Lastname,Firstname,Street,Area,HomePhone,Mobile,OtherPhone,Fax,Email);
            }
        }
    }
}
