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
    public class ProjectsController : Controller
    {
        private DB2 db = new DB2();

        // GET: Projects
        /// <summary>
        /// load the data from the database
        /// </summary>
        /// <param name="searchString">project name</param>
        /// <returns>details of projects</returns>
        [AuthorizeUserAcessLevel(UserRole = "Lecturer,HOD")]
        public ActionResult Index(string searchString)
        {
                      
            var names = from m in db.Projects
                        select m;
         
            if (!String.IsNullOrEmpty(searchString))
            {
                names = names.Where(s => s.Name.Contains(searchString));
            }
            else if (String.IsNullOrEmpty(searchString))
            {
                TempData["Msg1"] = "Enter valid value in the searchbar!";
            }

            return View(names);

        }
        // GET: Projects/Details/5
        /// <summary>
        /// get details according to the value passed by the parameter
        /// </summary>
        /// <param name="id">ProjectId</param>
        /// <returns>details of the project</returns>
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        /// <summary>
        /// Project Create View for the externel Client
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExternal()
        {
            ViewBag.ModuleName = db.Modules;
            TempData["ProjectCreate"] = null;
            return View();
        }

        // GET: Projects/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            getProjectId();
            ViewBag.ModuleName = db.Modules;
            //getCount();
            if ((string)Session["Position"] == "Lecturer")
            {
                ViewBag.Client = (string)Session["UserName"];
            }
            TempData["ProjectCreate"] = null;

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create the new project
        /// </summary>
        /// <param name="project">coloumn list in the project table</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ModuleId,Name,Description,Client,PreferedTechnologies")] Project project)
        {
            Debug.Write(project.ProjectId);
        string a= getProjectId();
            Debug.WriteLine(a);
          
            if (ModelState.IsValid)
            {
                project.ProjectId = a;
                try
                {
                    ViewBag.ModuleName = db.Modules;
                    db.Projects.Add(project);
                    db.SaveChanges();
                    getProjectId();
                    TempData["Msg1"] = "Successfully Inserted";
                    if (Session["UserName"] == null)
                    {
                        return RedirectToAction("Login", "Register");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    getProjectId();
                    return View(project);
                }
            }
            else
            {
                if ((string)Session["Position"] == "Lecturer")
                {
                    ViewBag.Client = (string)Session["UserName"];
                }
                ViewBag.ModuleName = db.Modules;
                getProjectId();
                return View(project);
            }
        }

        // GET: Projects/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleName = db.Modules;

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ModuleId,Name,Description,Client,PreferedTechnologies")] Project project)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg1"] = "Successfully Updated!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ModuleName = db.Modules;
                    return View("error", new HandleErrorInfo(e, "Projects", "Edit"));
                    throw e;
                }
            }
            else
            {
                return View(project);
            }

        }

        // GET: Projects/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string getProjectId() {
            string projectIdNew;
            var projectId = (from p in db.Projects
                             select p.ProjectId).Max();

            string pId = projectId.ToString();


            if (pId.Equals("") || pId.Equals(null))
            {
                projectIdNew = "P001";
            }
            else
            {
                int intNumber;
                string number = pId.Substring(pId.Length - 3, 3);
                int.TryParse(number, out intNumber);
                int newNumber = intNumber + 1;

                projectIdNew = "P00" + newNumber;
            }

            ViewBag.ProjectId = projectIdNew;
            return projectIdNew;
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
