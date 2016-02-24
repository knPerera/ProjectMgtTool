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
    public class ModulesController : Controller
    {
        private DB2 db = new DB2();

        // GET: Modules
        public ActionResult Index()
        {
            return View(db.Modules.ToList());
        }

        public ActionResult ViewAll(string searchString)
        {
            //List<string> ListItems = new List<string>();
            //ListItems.Add("Module Code");
            //ListItems.Add("Module Name");
            //ListItems.Add("Year");
            //ListItems.Add("Semester");
         

            var names = from m in db.Modules
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                names = names.Where(s => s.Description.Contains(searchString));
            }
            return View(names);
           // return View(db.Modules.ToList());
        }


        [HttpGet]
        public ActionResult ViewLecIC(string searchString)
        {
            ViewBag.Modules = db.Modules;
            var model = db.Modules.Where(d => d.LecturerIncharge == null).ToList();

            ViewBag.Lecturers = db.Lecturers;
            ViewBag.NLModules = db.Modules.Where(m => m.LecturerIncharge == "N/A" || m.LecturerIncharge == null);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //   var names = db.Modules.Where(m => m.LecturerIncharge == "N/A" || m.Description.Contains(searchString));
            //}
           
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewLecIC([Bind(Include = "LecturerIncharge")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
         
            return View(module);
        }



        //public ActionResult ViewLecIC(string SelectedModule) {


        //    var drafts = db.Modules.Where(d => d.LecturerIncharge == null).ToList();
           

        //    ViewBag.LecturerName = db.Lecturers.Select(c => new SelectListItem
        //    {
        //        Value = c.Name,
        //        Text = c.Name

        //    }).ToList();


        // GET: Modules/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
           
            ViewBag.Lecturers = db.Lecturers;

            List<string> ListItems = new List<string>();
            ListItems.Add("1");
            ListItems.Add("2");
            ListItems.Add("3");
            ListItems.Add("4");
         
            SelectList Years = new SelectList(ListItems);
            ViewData["Years"] = Years;
          

            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }


            ViewBag.Lecturers = db.Lecturers;

            List<string> ListItems = new List<string>();
            ListItems.Add("1");
            ListItems.Add("2");
            ListItems.Add("3");
            ListItems.Add("4");

            SelectList Years = new SelectList(ListItems);
            ViewData["Years"] = Years;
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        public ActionResult AssignLecIC(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }


            ViewBag.Lecturers = db.Lecturers;

            List<string> ListItems = new List<string>();
            ListItems.Add("1");
            ListItems.Add("2");
            ListItems.Add("3");
            ListItems.Add("4");

            SelectList Years = new SelectList(ListItems);
            ViewData["Years"] = Years;
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignLecIC([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }






        // GET: Modules/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
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
