using SEP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SEP.Controllers
{
    public class RegisterController : Controller
    {
        private DB2 db = new DB2();
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Index()
        {
            return View(db.Lecturers.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            TempData["Message1"] = null;
            Debug.Write("Mekta enne 1ta");
            return View();
        }


        [HttpPost]
        public ActionResult Login(string id, string Email)
        {

            bool remember = true;

            string query = "select * from dbo.Student where RegistrationNo = @p0 And Email =@p1";
            Student student2 = db.Students.SqlQuery(query, id, Email).SingleOrDefault();

            string query2 = "select * from dbo.Lecturer where LecturerId = @p0 And Email =@p1";
            Lecturer lecture2 = db.Lecturers.SqlQuery(query2, id, Email).SingleOrDefault();

            if (student2 != null && lecture2 == null)
            {

                Session["UserName"] = student2.Name;
                Session["Email"] = student2.Email;
                // var results = yourString.Split(new string[] { "is Marco and" }, StringSplitOptions.None);
                Session["Avatar"] = student2.Avatar;
                Session["CGPA"] = student2.CGPA;
                Session["CV"] = student2.CV;
                Session["ContactNo"] = student2.ContactNo;
                Session["Position"] = "student";
                Session["id"] = student2.RegistrationNo;
                if (Session["UserName"] != null)
                {

                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else if (student2 == null && lecture2 != null)
            {
                if (remember)
                {

                    FormsAuthentication.SetAuthCookie(lecture2.Name, remember);
                    FormsAuthentication.RedirectFromLoginPage(lecture2.Name, remember);

                }
                Session["UserName"] = lecture2.Name;
                Session["Email"] = lecture2.Email;
                Session["ContactNo"] = lecture2.ContactNo;
                Session["id"] = lecture2.LecturerId;
                Session["Avatar"] = lecture2.Avatar;
                if (lecture2.Name == "Auro")
                {
                    Session["Position"] = "HOD";

                }
                else
                {

                    Session["Position"] = "Lecturer";

                }
                if (Session["UserName"] != null)
                {
                    string Username = HttpContext.Profile.UserName;
                    Debug.Write(Username);
                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {

                    return HttpNotFound();
                }
            }

            //}
            TempData["Message1"] = " Erro Login in Please Check The Credenitials";
            return View();
        }

        public ActionResult Forgot()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Forgot(string Email)
        {
            List<object> parameterList3 = new List<object>();
            parameterList3.Add(new SqlParameter("@a1", Email));
            object[] parameters1231 = parameterList3.ToArray();

            string not = db.Database.SqlQuery<string>("select LecturerId from dbo.Lecturer where Email = @a1", parameters1231).FirstOrDefault<string>();
            string id = not;
            if (not == null)
            {

                List<object> parameterList2 = new List<object>();
                parameterList2.Add(new SqlParameter("@a1", Email));
                object[] parameters123 = parameterList2.ToArray();

                string not1 = db.Database.SqlQuery<string>("select RegistrationNo from dbo.Student where Email = @a1", parameters123).FirstOrDefault<string>();
                id = not1;
            }


            return RedirectToAction("Sendin", "Mail", new { Email1 = Email, Id1 = id });
        }
        [HttpGet]
        public ActionResult Extrnal()
        {

            return View();
        }

        public ActionResult Pending()
        {
            TempData["Pending"] = "U have to waot until the Head of the department accepts u're request";

            return View();
        }

        [HttpPost]
        public ActionResult Extrnal(string Email)
        {
            string query = "select * from dbo.Client where Email =@p1";
            List<object> parameterList2 = new List<object>();
            parameterList2.Add(new SqlParameter("@p1", Email));
            object[] parameters123 = parameterList2.ToArray();
            Client cl1 = db.Clients.SqlQuery(query, parameters123).SingleOrDefault();
            if (cl1 != null)
            {

                return RedirectToAction("Sendin", "Mail", new { Email1 = Email, Id1 = -1 + "", client1 = cl1.Name });

            }
            else
            {
                return RedirectToAction("Sendin", "Mail", new { Email1 = Email, Id1 = 0 + "" });
            }
        }
        public ActionResult LogOut()
        {
            if (Session["UserName"] != null)
            {

                Session.Clear();
                Session.Abandon();



                return RedirectToActionPermanent("Login", "Register");
            }

            return View();
        }

    }

}
