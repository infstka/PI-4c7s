using LR3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LR3.DataContext
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentsContext() : base(nameOrConnectionString: "DBContext")
        { }
    }
}