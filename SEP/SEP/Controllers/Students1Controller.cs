using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SEP.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using Rotativa;
using PagedList;
using PagedList.Mvc;

namespace SEP.Controllers
{
    public class Students1Controller : Controller
    {
        private DB2 db = new DB2();
        public ActionResult Index(string searchterm = null, int page = 1) {
            var model = (from r in db.Students
                         orderby r.Name ascending
                         where (r.Name.StartsWith(searchterm) || searchterm == null)
                        select r).ToPagedList(page ,3);
            return View(model);
        }

        // GET: Students1
        //public ActionResult Index(int page = 1)
        //{  
        //    return View(db.Students.ToPagedList(page , 4));
        //}

        // GET: Students1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students1/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
               public ActionResult Create([Bind(Include = "RegistrationNo,Name,Email,ContactNo,CGPA,Avatar,CV")] Student student , HttpPostedFileBase Avatar, HttpPostedFileBase CV)
        {
            
            Session["UserName"] = student.Name;
            Session["Email"] = student.Email;
           
            Session["CGPA"] = student.CGPA;
            Session["CV"] = student.CV;
            Session["ContactNo"] = student.ContactNo;
            Session["id"] = student.RegistrationNo;
            Session["Position"] = "student";
            if (ModelState.IsValid)
            {
                 if (Avatar != null && Avatar.ContentLength > 0 && CV != null && CV.ContentLength > 0)
                {
                    var ex = Path.GetExtension(Avatar.FileName);
                    var ex2 = Path.GetExtension(CV.FileName);
                    if (ex2.Equals(".PDF") || ex2.Equals(".pdf")) {
                        var fileName = Path.GetFileName(CV.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                        CV.SaveAs(path);
                        student.CV = path + "";

                    }
                    if (ex.Equals(".jpg") || ex.Equals(".jpeg") || ex.Equals(".GIF"))
                    {
                        var fileName = Path.GetFileName(Avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images2"), fileName);
                        string[] path2 = path.Split(new string[] { "SEP\\SEP" }, StringSplitOptions.None);
                        Avatar.SaveAs(path);
                        student.Avatar = path2[1] + "";
                    }
                }
                Session["Avatar"] = student.Avatar;
                db.Students.Add(student);
                db.SaveChanges();


               
                string query2 = "insert into  dbo.Notification(Name,Notification,Status,Loaded) values(@a3,@a4,@a5,@a6)";
                List<object> parameterList2 = new List<object>();
                parameterList2.Add(new SqlParameter("@a3", "Auro"));
                parameterList2.Add(new SqlParameter("@a4", student.Name + " Student Been Added To the SEP Tool "));
                parameterList2.Add(new SqlParameter("@a5", 2));
                parameterList2.Add(new SqlParameter("@a6", 2));
                object[] parameters12 = parameterList2.ToArray();
                int rs1 = db.Database.ExecuteSqlCommand(query2, parameters12);



                return RedirectToActionPermanent("Index", "Home");
            }

            return View(student);
        }
        [HttpPost]
        public ActionResult Create1(HttpPostedFileBase file) {
            
            return ViewBag;
        }
        // GET: Students1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationNo,Name,Email,ContactNo,CGPA,Avatar,CV")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            TempData["Message2"] = "Student";

            return View(student);
        }

        // POST: Students1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
