using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cms.Models;

namespace cms.Contexts
{
    public class CmsContext : DbContext
    {
        public CmsContext()
        {
            //Database.SetInitializer<CmsContext>(new CmsDBInitializer());
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
