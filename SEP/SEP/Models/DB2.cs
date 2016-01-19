using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SEP.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SEP.Models
{
    public class DB2 : DbContext
    {
        public DB2() : base("DB2")
        {

        }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<StudentStatus> Stu_Statuses { get; set; }

        public DbSet<LectureStatus> Lec_Statuses { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }




}