using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFandDB.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //relation * ---> *  there is list in Course class
        public List<Course> Courses { get; set; }
    }
}