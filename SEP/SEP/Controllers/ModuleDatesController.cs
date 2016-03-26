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
    public class ModuleDatesController : Controller
    {
        private DB2 db = new DB2();

        // GET: ModuleDates
        public ActionResult Index()
        {
            return View(db.ModuleDate.ToList());
        }

        // GET: ModuleDates/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDate moduleDate = db.ModuleDate.Find(id);
            if (moduleDate == null)
            {
                return HttpNotFound();
            }
            return View(moduleDate);
        }

        // GET: ModuleDates/Create
        public ActionResult Create()
        {
            ViewBag.ModuleName = db.Modules;
            return View();
        }

        // POST: ModuleDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleId,StartDate,EndDate,GroupFinalizeDate")] ModuleDate moduleDate)
        {
            ViewBag.ModuleName = db.Modules;
            if (ModelState.IsValid)
            {
                db.ModuleDate.Add(moduleDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moduleDate);
        }

        // GET: ModuleDates/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDate moduleDate = db.ModuleDate.Find(id);
            if (moduleDate == null)
            {
                return HttpNotFound();
            }
            return View(moduleDate);
        }

        // POST: ModuleDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleId,StartDate,EndDate,GroupFinalizeDate")] ModuleDate moduleDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moduleDate);
        }

        // GET: ModuleDates/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDate moduleDate = db.ModuleDate.Find(id);
            if (moduleDate == null)
            {
                return HttpNotFound();
            }
            return View(moduleDate);
        }

        // POST: ModuleDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ModuleDate moduleDate = db.ModuleDate.Find(id);
            db.ModuleDate.Remove(moduleDate);
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
