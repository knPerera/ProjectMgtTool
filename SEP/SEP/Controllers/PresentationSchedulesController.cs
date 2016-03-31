using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP.Models;
using System.Diagnostics;
using System.Globalization;

namespace SEP.Controllers
{
    public class PresentationSchedulesController : Controller
    {
        private DB2 db = new DB2();

        // GET: PresentationSchedules
        public ActionResult Index()
        {
            return View(db.PresentationSchedule.ToList());
        }

        // GET: PresentationSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationSchedule presentationSchedule = db.PresentationSchedule.Find(id);
            if (presentationSchedule == null)
            {
                return HttpNotFound();
            }
            return View(presentationSchedule);
        }

        // GET: PresentationSchedules/Create
        public ActionResult Create()
        {
            var id = (from p in db.PresentationSchedule
                                     select p.Id).Max();
            
            int sid = id + 1;
            ViewBag.shedid = sid.ToString();
            Debug.WriteLine(sid);
            ComboValues();
            ViewBag.Presentation = db.iterationInformations;
            return View();
        }

        // POST: PresentationSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Presentation,Date,Venue,TimePerGroup,Unit,StartTime,EndTime,Interval,NoOfGroups,NoOfPanels")] PresentationSchedule presentationSchedule)
        {
            Debug.WriteLine("llllll");
            ComboValues();
            ViewBag.Presentation = db.iterationInformations;

            ViewBag.stTime = from m in db.PresentationSchedule                              
                                 select m.StartTime;

           // ViewBag.stTime = startTime;

            if (ModelState.IsValid)
            {
                ViewBag.Presentation = db.iterationInformations;
                db.PresentationSchedule.Add(presentationSchedule);
                db.SaveChanges();
                return RedirectToAction("Confirm", "PresentationGroups");
            }

            return View(presentationSchedule);
        }

        // GET: PresentationSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationSchedule presentationSchedule = db.PresentationSchedule.Find(id);
            if (presentationSchedule == null)
            {
                return HttpNotFound();
            }
            return View(presentationSchedule);
        }

        // POST: PresentationSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Presentation,Date,Venue,TimePerGroup,Unit,StartTime,EndTime,Interval,NoOfGroups,NoOfPanels")] PresentationSchedule presentationSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentationSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentationSchedule);
        }

        // GET: PresentationSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationSchedule presentationSchedule = db.PresentationSchedule.Find(id);
            if (presentationSchedule == null)
            {
                return HttpNotFound();
            }
            return View(presentationSchedule);
        }

        // POST: PresentationSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresentationSchedule presentationSchedule = db.PresentationSchedule.Find(id);
            db.PresentationSchedule.Remove(presentationSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ComboValues()
        {
            ViewBag.Presentation = db.iterationInformations;


            List<SelectListItem> items2 = new List<SelectListItem>();
            items2.Add(new SelectListItem { Text = "min", Value = "min" });
            items2.Add(new SelectListItem { Text = "hour", Value = "hour" });
            ViewBag.Unit = items2;

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
