using SEP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP.Controllers
{
    public class DropdwnController : Controller
    {
        
        DB2 pk = new DB2();
        public ActionResult Not() {
        
            Debug.Write("Ambee"+ Session["UserName"]);
            List<object> GetNotify = new List<object>();
            GetNotify.Add(new SqlParameter("@a1", (string)Session["UserName"]));
            GetNotify.Add(new SqlParameter("@a3", 2));
            GetNotify.Add(new SqlParameter("@a2", 1));
            object[] Notification = GetNotify.ToArray();

            IList<string> Notify = pk.Database.SqlQuery<string>("select Notification from dbo.Notification where Name = @a1 and Status= @a2 and Loaded = @a3", Notification).ToList<string>();
                   

            string[] p = new string[Notify.Count];
            Notify.CopyTo(p, 0);
            for (int i = 0; i < Notify.Count; i++)
            {
                string UpdateNotifi = "Update  dbo.Notification set Status = @a3 where Notification = @a2";
                List<object> Upddatenotifi = new List<object>();
                Upddatenotifi.Add(new SqlParameter("@a3", 1));
                Upddatenotifi.Add(new SqlParameter("@a2", p[i]));

                object[] paraUpddatenotifi = Upddatenotifi.ToArray();
                int rs1 = pk.Database.ExecuteSqlCommand(UpdateNotifi, paraUpddatenotifi);


            }


            return Json(new { data = p });
            }
        public ActionResult NotLec()
        {
            List<object> parameterList3 = new List<object>();
            parameterList3.Add(new SqlParameter("@a1", (string)Session["UserName"]));
            parameterList3.Add(new SqlParameter("@a3", 2));
            parameterList3.Add(new SqlParameter("@a2", 2));
            object[] parameters1231 = parameterList3.ToArray();

            IList<string> not = pk.Database.SqlQuery<string>("select Request from dbo.Requests where Name = @a1 and Status= @a2 and Loaded = @a3",parameters1231).ToList<string>();


            string[] p = new string[not.Count];
            not.CopyTo(p, 0);
            for (int i = 0; i < not.Count; i++) {
                string query2 = "Update  dbo.Requests set Loaded = @a3 where Request = @a2";
                List<object> parameterList2 = new List<object>();
                parameterList2.Add(new SqlParameter("@a3", 1));
                parameterList2.Add(new SqlParameter("@a2", p[i]));

                object[] parameters12 = parameterList2.ToArray();
                int rs1 = pk.Database.ExecuteSqlCommand(query2, parameters12);


            }

            return Json(new { data = p });
            
        }

        public ActionResult AddLec(string Name)
        {
            Debug.Write(Name);
            string query2 = "Update  dbo.Requests set Status = @a3 where Request = @a2";
            List<object> parameterList2 = new List<object>();
            parameterList2.Add(new SqlParameter("@a3", 3));
            parameterList2.Add(new SqlParameter("@a2", Name));

            object[] parameters12 = parameterList2.ToArray();
            int rs1 = pk.Database.ExecuteSqlCommand(query2, parameters12);


            return Json(new { data = rs1});

        }

        public ActionResult RemoveLec(string Name)
        {
            Debug.Write(Name);
            string query2 = "Update  dbo.Requests set Status = @a3 where Request = @a2";
            List<object> parameterList2 = new List<object>();
            parameterList2.Add(new SqlParameter("@a3", -1));
            parameterList2.Add(new SqlParameter("@a2", Name));
            object[] parameters12 = parameterList2.ToArray();
            int rs1 = pk.Database.ExecuteSqlCommand(query2, parameters12);

            string query1 = "Delete From  dbo.Lecturer  where Name = @a2";
            List<object> parameterList1 = new List<object>();
            parameterList2.Add(new SqlParameter("@a3", -1));
            parameterList2.Add(new SqlParameter("@a2", Name));
            object[] parameters1 = parameterList1.ToArray();
            int rs2 = pk.Database.ExecuteSqlCommand(query1, parameters1);


            return Json(new { data = rs1 });

        }
        // GET: Dropdwn
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dropdwn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dropdwn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dropdwn/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dropdwn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dropdwn/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dropdwn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dropdwn/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


       
    }
}
