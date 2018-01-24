using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFandDB.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //relation  1----> *  
        public List<Assignment> Assignments { get; set; }

        //relation  *<--->* there is list in teacher class
        public List<Teacher> Teachers { get; set; }

        //relation  *<--->*  there is list in student class
        public List<Student> Students { get; set; }
    }
}