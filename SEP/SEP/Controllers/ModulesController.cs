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
    public class ModulesController : Controller
    {
        private DB2 db = new DB2();


        /// <summary>
        /// Get all modules details in the database
        /// </summary>
        /// <returns>List of all modules details </returns>
        public ActionResult Index()
        {
            if ((string)Session["Position"] == "student")
            {

            }
            return View(db.Modules.ToList());
        }
        /// <summary>
        /// Get modules details in database
        /// </summary>
        /// <param name="searchString">modulename</param>
        /// <returns>all the details of the module according to the paramatere</returns>
        public ActionResult ViewAll(string searchString)
        {
            var names = from m in db.Modules
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                names = names.Where(s => s.Description.Contains(searchString) || s.ModuleId.Contains(searchString));
            }
            return View(names);
        }

        /// <summary>
        /// Get module details
        /// </summary>
        /// <returns>list of modules that do not have a lecturer incharge</returns>
        [HttpGet]
        public ActionResult ViewLecIC()
        {
            ViewBag.Modules = db.Modules;
            var details = db.Modules.Where(d => d.LecturerIncharge == null).ToList();

            ViewBag.Lecturers = db.Lecturers;
            ViewBag.NLModules = db.Modules.Where(m => m.LecturerIncharge == "N/A" || m.LecturerIncharge == null);

            return View(details);
        }
        /// <summary>
        /// Set Lecturer Incharge for a module
        /// </summary>
        /// <param name="module">LecturerIncharge name</param>
        /// <returns></returns>
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

        /// <summary>
        /// get modules details
        /// </summary>
        /// <param name="id">moduleCode</param>
        /// <returns>details of the module according to the param</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Modules/Create
        public ActionResult Create()
        {
            ComboValues();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge,MaxLecPanel")] Module module)
        {

            if (db.Modules.Any(m => m.ModuleId.Equals(module.ModuleId)))
            {
                TempData["ErrMsg1"] = "Module Code Already Exists!";
                ModelState.AddModelError("Module Code Already exists", "Error");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Modules.Add(module);
                    db.SaveChanges();
                    TempData["Msg1"] = "Successfully Inserted";
                    return RedirectToAction("ViewAll");
                }
                catch
                {
                    ComboValues();
                    return View(module);
                }
            }
            else
            {
                ComboValues();
                return View(module);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            ComboValues();
            return View(module);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge,MaxLecPanel")] Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(module).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ViewAll");
                }
                catch
                {
                    ComboValues();
                    return View(module);
                }

            }
            else
            {
                ComboValues();
                return View(module);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            ComboValues();
            return View(module);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignLecIC([Bind(Include = "ModuleId,Description,MaxCount,Year,semester,LecturerIncharge,MaxLecPanel")] Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(module).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ViewAll");
                }
                catch
                {
                    ComboValues();
                    return View(module);
                }

            }
            else
            {
                ComboValues();
                return View(module);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
        /// <summary>
        /// 
        /// </summary>
        public void ComboValues()
        {
            ViewBag.Lecturers = db.Lecturers;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "1" });
            items.Add(new SelectListItem { Text = "2", Value = "2" });
            items.Add(new SelectListItem { Text = "3", Value = "3" });
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            ViewBag.Year = items;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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
