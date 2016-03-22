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
        /// <summary>
        /// According to the search term provide by user
        /// get the list of lecturers to a paged
        /// list with 5 elements per each page
        /// </summary>
        /// <param name="searchterm"></param>
        /// <param name="page"></param>
        /// <returns>"IEnumarable List Of Lecturers"</returns>
        [AuthorizeUserAcessLevel(UserRole = "Lecturer,HOD")]
        public ActionResult Index(string searchterm = null, int page = 1)
        {
            
            var model = (from r in db.Lecturers
                         orderby r.Name ascending
                         where (r.Name.Contains(searchterm) || searchterm == null)
                         select r).ToPagedList(page, 5);
            return View(model);

        }
       
        /// <summary>
        /// "According to the given id 
        /// provide the details of the
        /// Lecturer"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>"Returns the Object from Lecturer Entity type"</returns>
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
        /// <summary>
        /// Returns the create view
        /// works on the GET requests only
        /// </summary>
        /// <returns></returns>
        // GET: Lecturers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// If the given details are valid
        /// create a new Lecturer works only 
        /// on POST requests
        /// </summary>
        /// <param name="lecturer"></param>
        /// <param name="Avatar"></param>
        /// <param name="CV"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LecturerId,Name,Email,ContactNo,Module,Qualification,Avatar")] Lecturer lecturer, HttpPostedFileBase Avatar)
        {
            Session["UserName"] = lecturer.Name;
            Session["Email"] = lecturer.Email;
            Session["ContactNo"] = lecturer.ContactNo;
            Session["id"] = lecturer.LecturerId;
            Debug.Write(lecturer.Avatar + "Machnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn");
            Session["Position"] = "Lecturer";
            if (Avatar != null)
            {
                var extension = Path.GetExtension(Avatar.FileName);
                if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".GIF"))
                {
                    var fileName = Path.GetFileName(Avatar.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images2"), fileName);
                    string[] path2 = path.Split(new string[] { "SEP\\SEP" }, StringSplitOptions.None);
                    Debug.Write(path2[1]);
                    Avatar.SaveAs(path);
                    lecturer.Avatar = path2[1] + "";
                    TempData["error"] = null;
                }
                else if (!(extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".GIF")))
                {
                    TempData["error"] = "Please Use A Valid Avatar";
                }

            }
            if (ModelState.IsValid)
            {
                lecturer.Qualification = "Lecturer";
                lecturer.Module = "Pending";
                Session["Avatar"] = lecturer.Avatar;
                db.Lecturers.Add(lecturer);
                db.SaveChanges();

                string time = DateTime.Now.ToString("HH:mm:ss tt");
                string query = "insert into  dbo.Requests(Name,Request,Status,Loaded) values(@a1,@a2,@a3,@a4)";
                List<object> parameterList = new List<object>();
                parameterList.Add(new SqlParameter("@a1", "Auro"));
                parameterList.Add(new SqlParameter("@a2", lecturer.Name + "Been Added To the SEP Tool CUT" + lecturer.Avatar + "CUT" + time));
                parameterList.Add(new SqlParameter("@a3", 2));
                parameterList.Add(new SqlParameter("@a4", 2));
                object[] parameters123 = parameterList.ToArray();
                int rs = db.Database.ExecuteSqlCommand(query, parameters123);

                return RedirectToActionPermanent("Pending", "Register");
            }

            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        /// <summary>
        /// Find if theres any Lecturers for the given id 
        /// if exists returning the Edit view
        /// works on GET methods
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lecturer type object</returns>
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
        /// <summary>
        /// If the given details are valid 
        /// change the attributes of the selected Lecturer object
        /// </summary>
        /// <param name="lecturer"></param>
        /// <param name="Avatar"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Find if theres any Lecturers for the given id 
        /// if exists returning the Delete view
        /// works on GET methods
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lecturer type object</returns>
        // GET: Lecturers/Delete/5
        [AuthorizeUserAcessLevel(UserRole ="HOD")]
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

        /// <summary>
        /// If the user wants to delete the Lecturer object 
        /// this will do it 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUserAcessLevel(UserRole = "HOD")]
        public ActionResult DeleteConfirmed(string id)
        {
            Lecturer lecturer = db.Lecturers.Find(id);
            db.Lecturers.Remove(lecturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// After performing the action relate to database 
        /// to turn of the db connection
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
