using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP.Models;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using PagedList;

namespace SEP.Controllers
{
    public class LecturersController : Controller
    {
        private DB2 db = new DB2();

        // GET: Lecturers
        public ActionResult Index(string searchterm = null, int page = 1)
        {
            string position = (string)Session["UserName"];
            bool p = db.Modules.Any(ac => ac.LecturerIncharge.Equals(position));

            if (p)
            {
                Session["LecIN"] = true;
                Debug.Write("Hari Eka");
            }
            else
            {
                Session["LecIN"] = false;
                Debug.Write("Wardi Eka");
            }
            var model = (from r in db.Lecturers
                         orderby r.Name ascending
                         where (r.Name.Contains(searchterm) || searchterm == null)
                         select r).ToPagedList(page, 3);
            return View(model);

            
           

        }

        // GET: Lecturers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // GET: Lecturers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LecturerId,Name,Email,ContactNo,Module,Qualification,Avatar")] Lecturer lecturer, HttpPostedFileBase Avatar)
        {       
                Session["UserName"] = lecturer.Name;
                Session["Email"] = lecturer.Email;
                Session["ContactNo"] = lecturer.ContactNo;
                Session["id"] = lecturer.LecturerId;
            if (ModelState.IsValid)
            {

                if (Avatar != null)
                {
                    var ex = Path.GetExtension(Avatar.FileName);
                    if (ex.Equals(".jpg") || ex.Equals(".jpeg") || ex.Equals(".GIF"))
                    {
                        var fileName = Path.GetFileName(Avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images2"), fileName);
                        string[] path2 = path.Split(new string[] { "SEP\\SEP" }, StringSplitOptions.None);
                        Debug.Write(path2[1]);
                        Avatar.SaveAs(path);
                        lecturer.Avatar = path2[1] + "";
                    }

                }
                Session["Avatar"] = lecturer.Avatar;
                db.Lecturers.Add(lecturer);
                db.SaveChanges();
                string query = "insert into  dbo.Requests(Name,Request,Status,Loaded) values(@a1,@a2,@a3,@a4)";
                List<object> parameterList = new List<object>();
                parameterList.Add(new SqlParameter("@a1", "Auro"));
                parameterList.Add(new SqlParameter("@a2", lecturer.Name + "Been Added To the SEP Tool "));
                parameterList.Add(new SqlParameter("@a3", 2));
                parameterList.Add(new SqlParameter("@a4", 2));
                object[] parameters123 = parameterList.ToArray();
                int rs = db.Database.ExecuteSqlCommand(query, parameters123);



                return RedirectToActionPermanent("Index","Home");
            }

            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
       
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LecturerId,Name,Email,ContactNo,Module,Qualification,Avatar")] Lecturer lecturer, HttpPostedFileBase Avatar)
        {
            if (ModelState.IsValid)
            {

                if (Avatar != null) 
                {
                    var ex = Path.GetExtension(Avatar.FileName);
                    if (ex.Equals(".jpg") || ex.Equals(".jpeg") || ex.Equals(".GIF"))
                    {
                        var fileName = Path.GetFileName(Avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images2"), fileName);
                        string[] path2 = path.Split(new string[] { "SEP\\SEP" }, StringSplitOptions.None);
                        Debug.Write(path2[1]);
                        Avatar.SaveAs(path);
                        lecturer.Avatar = path2[1] + "";
                    }

                }
                db.Entry(lecturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecturer);
        }

        // GET: Lecturers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            TempData["Message2"] = "Lec";

            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Lecturer lecturer = db.Lecturers.Find(id);
            db.Lecturers.Remove(lecturer);
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
