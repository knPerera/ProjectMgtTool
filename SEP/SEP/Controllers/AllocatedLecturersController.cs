using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SEP.Models;
using System.Collections.Generic;

namespace SEP.Controllers
{
    public class AllocatedLecturersController : Controller
    {
        private DB2 db = new DB2();

        // GET: AllocatedLecturers
      
        public ActionResult Index(string Search)
        {
            
                return View (db.AllocatedLecturers.Where(x => x.Supervisors.StartsWith(Search)|| Search==null).ToList());

            }

        // GET: AllocatedLecturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedLecturers allocatedLecturers = db.AllocatedLecturers.Find(id);
            if (allocatedLecturers == null)
            {
                return HttpNotFound();
            }
            return View(allocatedLecturers);
        }

        internal bool UserExists(string username)
        {
            throw new NotImplementedException();
        }
       

        // GET: AllocatedLecturers/Create
        public ActionResult Create()
        {
           
            ViewBag.Supervisor = new SelectList(db.Groups, "GroupID", "Supervisor");
            ViewBag.Lecturers = new SelectList(db.Lecturers, "Name", "Name");

            return View();
        }

        // POST: AllocatedLecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Supervisors,Lecturers")] AllocatedLecturers allocatedLecturers)
        {
            var vari = allocatedLecturers.Supervisor;



            if (db.AllocatedLecturers.Any(ac => ac.Lecturers.Equals(allocatedLecturers.Lecturers)) && db.AllocatedLecturers.Any(acs=>acs.Supervisors.Equals(allocatedLecturers.Supervisors)))
            
            {
                ModelState.AddModelError("Already exists", "Error");
                return RedirectToAction("Edit", "Groups1");

            }


            if (ModelState.IsValid)
            {
                    
                db.AllocatedLecturers.Add(allocatedLecturers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Supervisor = new SelectList(db.AllocatedLecturers, "GroupID", "Supervisor", allocatedLecturers.Supervisors);
            ViewBag.Lecturers = new SelectList(db.AllocatedLecturers, "Lecturers", "Lecturers", allocatedLecturers.Lecturers);
                

            return View(allocatedLecturers);
        }

        // GET: AllocatedLecturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedLecturers allocatedLecturers = db.AllocatedLecturers.Find(id);
            if (allocatedLecturers == null)
            {
                return HttpNotFound();
            }
            return View(allocatedLecturers);
        }

        // POST: AllocatedLecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Supervisors,Lecturers")] AllocatedLecturers allocatedLecturers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocatedLecturers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allocatedLecturers);
        }

        // GET: AllocatedLecturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedLecturers allocatedLecturers = db.AllocatedLecturers.Find(id);
            if (allocatedLecturers == null)
            {
                return HttpNotFound();
            }
            return View(allocatedLecturers);
        }

        // POST: AllocatedLecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocatedLecturers allocatedLecturers = db.AllocatedLecturers.Find(id);
            db.AllocatedLecturers.Remove(allocatedLecturers);
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

        public JsonResult GetStudents(string term)
        {
            DB2 db = new DB2();
            List <string> students;

            students = db.AllocatedLecturers.Where(x => x.Supervisors.StartsWith(term)).Select(y => y.Supervisors).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }


    }
}
