
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFandDB.Models;

namespace EFandDB.Controllers
{
    public class TeachersController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Teachers
        public ActionResult Index(string orderBy)
        {
            List<Teacher> teacherList = new List<Teacher>();//we need teacher list to display

            if (string.IsNullOrEmpty(orderBy))
            {
                ViewBag.orderNameBy = "NameA";
                teacherList = db.Teachers.ToList();
            }
            else
            {
                switch (orderBy)
                {
                    case "NameA":
                        ViewBag.orderBy = "NameD";
                        teacherList = db.Teachers.OrderBy(t => t.Name).ToList();//A --> Z
                        break;
                    case "NameD":
                        ViewBag.orderBy = "NameA";
                        teacherList = db.Teachers.OrderByDescending(t => t.Name).ToList();//Z --> A
                        break;
                    default:
                        break;
                }
            }//To teacher view to display as ordering up or dawon 
            return View(teacherList);
        }

        [HttpGet]
        public ActionResult CourseToTeacher(int? tId,int? cId)
        {
            Course course = db.Courses.SingleOrDefault(c => c.Id == cId);
            Teacher teacher = db.Teachers.SingleOrDefault(t => t.Id == tId);
            teacher.Courses.Add(course);//added to list of teacher
            db.SaveChanges();

            return RedirectToAction("Details",new { id=tId});
        }

        [HttpGet]
        public ActionResult AddCourseToTeacher(int? tId)
        {
            if (tId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Teacher> teacher = new List<Teacher>();
            ViewBag.tId = tId; //send id teacher in details side as confirm about the name of teacher
            return View(teacher);
        }
        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Teacher teacher = db.Teachers.Find(id);//we have to change the code to be include courses
            Teacher teacher = db.Teachers.Include("Courses").SingleOrDefault(t=>t.Id==id);//To not have null as courses
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,City")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
