using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cms.Models;
using cms.Contexts.Mappings;

namespace cms.Contexts
{
    public class CmsContext : DbContext
    {
        static CmsContext()
        {
            Database.SetInitializer<CmsContext>(null);
            //Database.SetInitializer<CmsContext>(new CmsDBInitializer());
        }

        public CmsContext()
            : base("Name=CmsContext")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ToDo> ToDoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new ToDoMap());
        }
    }
}
