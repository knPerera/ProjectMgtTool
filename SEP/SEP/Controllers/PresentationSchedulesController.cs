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
            Debug.WriteLine("ooooooooooo");
            //   TimeSpan
          //  CalculateGroups(startTime, endTime, oneGroupTime);
            ComboValues();
       //  int result= CalculateGroups("01:30 PM", "04:00 PM",20);        
            return View();
        }

        private int CalculateGroups(string startTime, string endTime,int oneGroupTime)
        {
            Debug.WriteLine("brrrrrrr");
            Debug.WriteLine(startTime);
            Debug.WriteLine(endTime);
            Debug.WriteLine(oneGroupTime);


            int groups;
            //string startUnit = startTime.Substring(startTime.Length - 2, 2);
            //string endUnit = endTime.Substring(endTime.Length - 2, 2);

            //string start = startTime.Substring(0, 5);
            //string end = endTime.Substring(0, 5);

            //int startHour = int.Parse(startTime.Substring(0, 2));
            //int endHour = int.Parse(endTime.Substring(0, 2));

            //int startMin = int.Parse(startTime.Substring(3, 2));
            //int endMin = int.Parse(endTime.Substring(3, 2));

            int difMin, difHr;
            int totMins = 40 ;

          //  Debug.WriteLine(endHour + endUnit);
            //int h3 = DateTime.Parse(endHour + endUnit).Hour;

            //if (startUnit.Equals("AM") && endUnit.Equals("AM"))
            //{
            //    if (endMin < startMin)
            //    {
            //        difMin = (endMin + 60) - startMin;
            //        difHr = (endHour - 1) - startHour;
            //        totMins = difMin + (difHr * 60);
            //        Debug.WriteLine("Wede Hariiii " + totMins);
            //    }
            //    else if (startHour > endHour)
            //    {
            //        Debug.WriteLine("Weradiiiiiiiiii");
            //    }
            //    else
            //    {
            //        difMin = endMin - startMin;
            //        difHr = endHour - startHour;
            //        totMins = difMin + (difHr * 60);
            //    }
            //}

            //else if (startUnit.Equals("PM") && endUnit.Equals("AM"))
            //{
            //    Debug.WriteLine("Errrorrrrrrr! weradiiiii");
            //}

            //else if (startUnit.Equals("AM") && endUnit.Equals("PM"))
            //{

            //    int end24 = DateTime.Parse(endHour + endUnit).Hour;

            //    if (endMin < startMin)
            //    {
            //        difMin = (endMin + 60) - startMin;
            //        difHr = (end24 - 1) - startHour;
            //        totMins = difMin + (difHr * 60);
            //        Debug.WriteLine("22222222Wede Hariiii " + totMins);
            //    }
            //    else
            //    {
            //        difMin = endMin - startMin;
            //        difHr = end24 - startHour;
            //        totMins = difMin + (difHr * 60);
            //    }
            //}

            //else if (startUnit.Equals("PM") && endUnit.Equals("PM"))
            //{
            //    if (endMin < startMin)
            //    {
            //        difMin = (endMin + 60) - startMin;
            //        difHr = (endHour - 1) - startHour;
            //        totMins = difMin + (difHr * 60);
            //        Debug.WriteLine("22222222Wede Hariiii " + totMins);
            //    }
            //    else if (startHour > endHour)
            //    {
            //        Debug.WriteLine("Weradiiiiiiiiii");
            //    }
            //    else
            //    {
            //        difMin = endMin - startMin;
            //        difHr = endHour - startHour;
            //        totMins = difMin + (difHr * 60);
            //        Debug.WriteLine("Wede Hariiiiii333333333");
            //    }

          //  }

            //groups = totMins / oneGroupTime;
            return 20;
         //   throw new NotImplementedException();
        }

        // POST: PresentationSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Presentation,Date,Venue,TimePerGroup,Unit,StartTime,EndTime,NoOfGroups,NoOfPanels")] PresentationSchedule presentationSchedule)
        {

            Debug.WriteLine("llllll");
            ComboValues();
            if (ModelState.IsValid)
            {
                db.PresentationSchedule.Add(presentationSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "Id,Presentation,Date,Venue,TimePerGroup,Unit,StartTime,EndTime,NoOfGroups,NoOfPanels")] PresentationSchedule presentationSchedule)
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
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Proposal Presentation", Value = "1" });
            items.Add(new SelectListItem { Text = "Iteration 1 Presentation", Value = "2" });
            items.Add(new SelectListItem { Text = "Iteration 2 Presentation", Value = "3" });
            items.Add(new SelectListItem { Text = "Final Presentation", Value = "4" });
            ViewBag.Presentation = items;

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
