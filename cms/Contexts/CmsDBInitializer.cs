using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cms.Contexts
{
    public class CmsDBInitializer : DropCreateDatabaseIfModelChanges<CmsContext>
    {
        public override void InitializeDatabase(CmsContext context)
        {
            //base.InitializeDatabase(context);

            string sql = string.Empty;

            //Create Person Table
            sql = "DROP TABLE IF EXISTS [People];";
            sql += "CREATE TABLE[People] (";
            sql += "[Id] nvarchar(100) NOT NULL";
            sql += ", [Lastname] nvarchar(100) NOT NULL";
            sql += ", [Firstname] nvarchar(100) NOT NULL";
            sql += ", CONSTRAINT[sqlite_master_PK_People] PRIMARY KEY([Id])";
            sql += "); ";
            context.Database.ExecuteSqlCommand(sql);

            //Create Job Table
            sql = "DROP TABLE IF EXISTS [Jobs];";
            sql += "CREATE TABLE[Jobs] (";
            sql += "[Id] nvarchar(100) NOT NULL";
            sql += ", [Description] nvarchar(200) NOT NULL";
            sql += ", [Implemented] datetime NOT NULL";
            sql += ", CONSTRAINT[sqlite_master_PK_People] PRIMARY KEY([Id])";
            sql += "); ";
            context.Database.ExecuteSqlCommand(sql);

        }

        protected override void Seed(CmsContext context)
        {
            //base.Seed(context);
        }
    }
}
