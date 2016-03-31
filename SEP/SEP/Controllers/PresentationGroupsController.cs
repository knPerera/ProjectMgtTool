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

namespace SEP.Controllers
{
    public class PresentationGroupsController : Controller
    {
        private DB2 db = new DB2();

        // GET: PresentationGroups
        public ActionResult Index()
        {

            return View(db.PresentationGroups.ToList());
        }
        public ActionResult Confirm()
        {
            
            return View();
        }


        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "Id,ScheduleId,PanelNo,GroupId,ProjectId,Supervisor,StartTime,EndTime")] PresentationGroups presentationGroups)
        {

            var id = (from p in db.PresentationSchedule
                      select p.Id).Max();

            PresentationSchedule schedule = (from m in db.PresentationSchedule
                                             where m.Id == id
                                             select m).FirstOrDefault<PresentationSchedule>();

            int newHr = 0;

            DateTime stTm = schedule.StartTime;
            DateTime endTm = schedule.EndTime;
       
            string stTm2 = stTm.ToString("HH:mm:ss");
            string endTm2 = endTm.ToString("HH:mm:ss");

             
            int sid = schedule.Id;
            int noGrps = schedule.NoOfGroups;

            int oneGrpTm = schedule.TimePerGroup;
            int intvl = schedule.Interval;

         //   int totGap = (oneGrpTm + intvl);
           // int totMin = (oneGrpTm + intvl) * noGrps;
           // string totMinStr = totMin.ToString();

            string startTm = stTm.ToString("hh:mm tt");
            string endTime = endTm.ToString("hh:mm tt");

            string grpStartTime = startTm;
            string grpEndTime = null;

            TimeSpan startTime = TimeSpan.Parse(stTm2);
            TimeSpan tspOneGrp = TimeSpan.FromMinutes(oneGrpTm);
            TimeSpan tspIntervl = TimeSpan.FromMinutes(intvl);
       
            TimeSpan sumtime = startTime + tspOneGrp;
            DateTime dtmSum = Convert.ToDateTime(sumtime.ToString());
            string GrpSchedTime = dtmSum.ToString("hh:mm tt");

          //  string grpi = "A";
            for (int i = 0; i < noGrps; i++)
            {
                Debug.WriteLine("athuleeeeeee  ");
               
                presentationGroups.ScheduleId = sid;
          //      presentationGroups.PanelNo = 1;
            //    presentationGroups.GroupId = grpi;
             //   presentationGroups.ProjectId = "P003";
            //    presentationGroups.Supervisor = "Amal";
                presentationGroups.StartTime = grpStartTime;
                presentationGroups.EndTime = GrpSchedTime;

            
                db.PresentationGroups.Add(presentationGroups);
                db.SaveChanges();

                string lastEndTm = dtmSum.ToString("HH:mm:ss");

                TimeSpan newStartTime = TimeSpan.Parse(lastEndTm);

                sumtime = newStartTime + tspIntervl;
                dtmSum = Convert.ToDateTime(sumtime.ToString());
                grpStartTime = dtmSum.ToString("hh:mm tt");

                TimeSpan enddtm = sumtime + tspOneGrp;
               DateTime dtmSum2 = Convert.ToDateTime(enddtm.ToString());              
                GrpSchedTime = dtmSum2.ToString("hh:mm tt");

                Debug.WriteLine(sumtime+"sumtime");
                Debug.WriteLine("onegrp "+ tspOneGrp);
                Debug.WriteLine(enddtm);
                Debug.WriteLine(dtmSum2);
                Debug.WriteLine(GrpSchedTime);

                dtmSum = dtmSum2;
                Debug.WriteLine(dtmSum+"dtmsum");

                //   sumtime = sumtime + tspOneGrp;
                // dtm1 = Convert.ToDateTime(sumtime.ToString());
                //MinTm = dtm1.ToString("hh:mm tt");
            }
                     
                return RedirectToAction("Create");          
           // return View(presentationGroups);
        }


        // GET: PresentationGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationGroups presentationGroups = db.PresentationGroups.Find(id);
            if (presentationGroups == null)
            {
                return HttpNotFound();
            }
            return View(presentationGroups);
        }

        // GET: PresentationGroups/Create
        public ActionResult Create()
        {
            //var lists = (from p in db.PresentationSchedule.ToList()
            //                 select p).Max();

            var id = (from p in db.PresentationSchedule
                      select p.Id).Max();

            ViewBag.timetable = (from p in db.PresentationSchedule
                                 where p.Id == id
                                 select p).ToList();

            Debug.WriteLine(id);


            ViewBag.ModuleName = db.Modules;
            return View();
        }

        // POST: PresentationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ScheduleId,PanelNo,GroupId,ProjectId,Supervisor,StartTime,EndTime")] PresentationGroups presentationGroups)
        {
            ViewBag.ModuleName = db.Modules;
            if (ModelState.IsValid)
            {
                ViewBag.ModuleName = db.Modules;
                db.PresentationGroups.Add(presentationGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presentationGroups);
        }

        // GET: PresentationGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationGroups presentationGroups = db.PresentationGroups.Find(id);
            if (presentationGroups == null)
            {
                return HttpNotFound();
            }
            return View(presentationGroups);
        }

        // POST: PresentationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ScheduleId,PanelNo,GroupId,ProjectId,Supervisor,StartTime,EndTime")] PresentationGroups presentationGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentationGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentationGroups);
        }

        // GET: PresentationGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentationGroups presentationGroups = db.PresentationGroups.Find(id);
            if (presentationGroups == null)
            {
                return HttpNotFound();
            }
            return View(presentationGroups);
        }

        // POST: PresentationGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresentationGroups presentationGroups = db.PresentationGroups.Find(id);
            db.PresentationGroups.Remove(presentationGroups);
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
