using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFandDB.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //relation   * ---> 1
        public Course AssigmedTo { get; set; }

    }
}