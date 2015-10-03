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
            sql += ", [Street] nvarchar(100) NULL";
            sql += ", [StreetNumber] nvarchar(10) NULL";
            sql += ", [Area] nvarchar(100) NULL";
            sql += ", [PostalCode] nvarchar(10) NULL";
            sql += ", [HomePhone] nvarchar(10) NULL";
            sql += ", [Mobile] nvarchar(10) NULL";
            sql += ", [OtherPhone] nvarchar(10) NULL";
            sql += ", [Fax] nvarchar(10) NULL";
            sql += ", [Email] nvarchar(100) NULL";
            sql += ", [Remarks] nvarchar(500) NULL";
            sql += ", CONSTRAINT[sqlite_master_PK_People] PRIMARY KEY([Id])";
            sql += "); ";
            context.Database.ExecuteSqlCommand(sql);

            //Create Job Table
            sql = "DROP TABLE IF EXISTS [Jobs];";
            sql += "CREATE TABLE[Jobs] (";
            sql += "[Id] nvarchar(100) NOT NULL";
            sql += ", [PersonId] nvarchar(100) NOT NULL";
            sql += ", [Description] nvarchar(200) NOT NULL";
            sql += ", [Implemented] datetime NOT NULL";
            sql += ", [Amount] money NOT NULL";
            sql += ", [Remarks] nvarchar(500) NULL";
            sql += ", CONSTRAINT[sqlite_master_PK_Jobs] PRIMARY KEY([Id])";
            sql += ", FOREIGN KEY(PersonId) REFERENCES People(Id) ON DELETE CASCADE";
            sql += "); ";
            context.Database.ExecuteSqlCommand(sql);

            //Create ToDo Table
            sql = "DROP TABLE IF EXISTS [ToDoes];";
            sql += "CREATE TABLE[ToDoes] (";
            sql += "[Id] nvarchar(100) NOT NULL";
            sql += ", [PersonId] nvarchar(100) NULL";
            sql += ", [Description] nvarchar(200) NOT NULL";
            sql += ", [ToDoDate] datetime NOT NULL";
            sql += ", [Done] bit NOT NULL";
            sql += ", CONSTRAINT[sqlite_master_PK_ToDos] PRIMARY KEY([Id])";
            sql += ", FOREIGN KEY(PersonId) REFERENCES People(Id) ON DELETE CASCADE";
            sql += "); ";
            context.Database.ExecuteSqlCommand(sql);
        }

        protected override void Seed(CmsContext context)
        {
            //base.Seed(context);
        }
    }
}
