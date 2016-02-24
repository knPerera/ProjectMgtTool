using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP.Models;

namespace SEP.Controllers
{
    public class GroupsController : Controller
    {
        private DB2 db = new DB2();

        // GET: Groups
        public ActionResult Index(string SearchBy,string Search)
        {

            if (SearchBy == "projectid")
            {
                return View(db.Groups.Where(x => x.ProjectID == Search || Search == null).ToList());
            }
            else if (SearchBy == "supervisor")
            {
                return View(db.Groups.Where(x => x.Supervisor == Search || Search == null).ToList());

            }else if(SearchBy == "projectid" & SearchBy == "supervisor")
            {
                return View(db.Groups.Where(x => x.GroupID.StartsWith(Search) || Search == null).ToList());

            }
            else {
                return View(db.Groups.Where(x => x.GroupID.StartsWith(Search) || Search == null).ToList());
            }
        }
        // GET: Groups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Modules, "ModuleId", "ModuleId");
            ViewBag.Supervisor = new SelectList(db.Lecturers, "LecturerId", "Name");
            
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            if (db.Groups.Any(ac => ac.GroupID.Equals(group.GroupID)))
            {
                ModelState.AddModelError("Already exists", "Error");
                return RedirectToAction("Index", "Groups1");

            }
            if (ModelState.IsValid)
                {
                
               
                    db.Groups.Add(group);
                    db.SaveChanges();
                    ModelState.Clear();     
                    return RedirectToAction("index");

            }
         //   ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "GroupId", group.GroupID);
            ViewBag.ProjectId = new SelectList(db.Groups, "GroupId", "ProjectID", group.ProjectID);
            ViewBag.Supervisor = new SelectList(db.Groups, "GroupId", "Supervisor", group.Supervisor);

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Groups, "GroupId", "ProjectID", group.ProjectID);
            ViewBag.Supervisor = new SelectList(db.Groups, "GroupId", "Supervisor", group.Supervisor);

            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,ProjectID,Supervisor")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
        public JsonResult CheckForDuplication(String names)
        {
            var data = db.Groups.Where(p => p.GroupID.Equals(names, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (data != null)
            {
                return Json("Sorry, this name already exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet); 
            }
        }
    }
}




