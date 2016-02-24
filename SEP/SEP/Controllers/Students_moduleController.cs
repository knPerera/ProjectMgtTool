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
using System.Data.SqlClient;
using System.Web.Helpers;

namespace SEP.Controllers
{
    public class Students_moduleController : Controller
    {
        private DB2 db = new DB2();

        // GET: Students_module
        public ActionResult Index(string searchTerm=null)
        {
            var model = (from r in db.Students
                         orderby r.Name ascending
                         where (r.Name.Contains(searchTerm) || searchTerm == null)
                         select r);
          //  IEnumerable<Student> st1 = db.Students.SqlQuery("select * from dbo.Student").ToList<Student>();
            
            return View(model);
        }
        
        public ActionResult getDetails(Student er)
        {
            return View();
        }
        //********************************************************
        //get student into view
        //********************************************************
        public ActionResult GetStudent(string id) {
            Debug.Write(id);
            string qry = "select * from dbo.Student where Name = @a1";
            List<object> parameterList2 = new List<object>();
            parameterList2.Add(new SqlParameter("@a1", id));
            object[] parameters12 = parameterList2.ToArray();

            Student st1 = db.Students.SqlQuery(qry, parameters12).FirstOrDefault();

            Student st = new Student
            {
                Name = st1.Name,
                CGPA=st1.CGPA,
                RegistrationNo=st1.RegistrationNo,
                ContactNo=st1.ContactNo,
                Email=st1.Email,
                Avatar=st1.Avatar
            };
            return Json(new { student = st},JsonRequestBehavior.AllowGet);
            
        }



        //Method to draw a chart
        //public ActionResult DrawChart(string stuId)
        //{
        //    string qry = "select * from dbo.languageProficiency where RegistrationNum= @r1";
        //    List<object> parameterList5 = new List<object>();
        //    parameterList5.Add(new SqlParameter("@r1", stuId));
        //    var dbdata = db.Database.SqlQuery<string>(qry, stuId);
        //    var myChart = new Chart(width: 600, height: 400)
        //       .AddTitle("Product Sales")
        //       .DataBindTable(dataSource: dbdata, xField: "Name")
        //    


        //********************************************************
        //Adding loged user to group
        //********************************************************
        public ActionResult FillMe(string reg) {
            int rset = 0;
          //  int zeroVal = 0; 

            int grpID = 1;
            Debug.Write("Here we areee");

            string qryGetMaxId = "select max(Id) from StudentGroupeList";

            var val = db.Database.SqlQuery<int>(qryGetMaxId);

            int no = Convert.ToInt32(val);

            if (no != 0)
            {
                grpID = no + 1;
            }

            string qryAddMe = "insert into dbo.StudentGroupeList(StuId,Module, GroupNo) values(@x,@y,@z)";
            List<object> parameterListMe = new List<object>();
            parameterListMe.Add(new SqlParameter("@a", reg));
            parameterListMe.Add(new SqlParameter("@b", "SEP"));
            parameterListMe.Add(new SqlParameter("@c", grpID));

            object[] parameters10 = parameterListMe.ToArray();

            rset = db.Database.ExecuteSqlCommand(qryAddMe, parameters10);

            Debug.Write("Affected By Meeeeeeeeeeeeeeeeeee"+rset);

            return Json(rset, JsonRequestBehavior.AllowGet);
        }
        //********************************************************
        //Adding Student to group
        //********************************************************
        public ActionResult addToGroup(string sreg)
        {
            

            string qry3 = "select GroupNo from dbo.StudentGroupeList where StuId = @p0";
            List<object> parameterList4 = new List<object>();
            parameterList4.Add(new SqlParameter("@p0", sreg));
            object[] parameters1312 = parameterList4.ToArray();
            string stw = db.Database.SqlQuery<string>(qry3, parameters1312).FirstOrDefault();
            Debug.Write("Group no of the student: " + stw);

            string qry5 = "select count(StuId) from dbo.StudentGroupeList where GroupNo = @a1";
            List<object> parameterList5 = new List<object>();
            parameterList5.Add(new SqlParameter("@a1", stw));
            object[] parameters1311 = parameterList5.ToArray();
            int grpCount = db.Database.SqlQuery<int>(qry5, parameters1311).FirstOrDefault<int>();
            Debug.Write("Count of students : " + grpCount);
            // var $
            int rs = 0;
            int noVal = 0;
            if ((grpCount + 1) < 5)
            {
                string qry2 = "insert into dbo.StudentGroupeList(StuId,Module, GroupNo) values(@a,@b,@c)";
                List<object> parameterList3 = new List<object>();
                parameterList3.Add(new SqlParameter("@a", sreg));
                parameterList3.Add(new SqlParameter("@b", "SEP"));
                parameterList3.Add(new SqlParameter("@c", "SEP_002"));
                object[] parameters13 = parameterList3.ToArray();

                 rs = db.Database.ExecuteSqlCommand(qry2, parameters13);

                Debug.Write("No of affected rows " + rs);

                return Json(grpCount, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(noVal, JsonRequestBehavior.AllowGet);
            }
           // return Json(5, JsonRequestBehavior.AllowGet);
        }
        // GET: Students_module/Details/5
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

        // GET: Students_module/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students_module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationNo,Name,Email,ContactNo,CGPA,Avatar,CV,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students_module/Edit/5
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

        // POST: Students_module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationNo,Name,Email,ContactNo,CGPA,Avatar,CV,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students_module/Delete/5
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
            return View(student);
        }

        // POST: Students_module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // Search funtion
        public ActionResult searchStudent(string key) {



            string qrysearch = "select Name from dbo.Student where Name LIKE '%' + @q1 + '%'";
        
            Debug.Write("Key"+key);

            List<object> parameterListSearch = new List<object>();
            parameterListSearch.Add(new SqlParameter("@q1", key));
            object[] p1 = parameterListSearch.ToArray();
            IList<string>rs = db.Database.SqlQuery<string>(qrysearch, p1).ToList<string>();

            Array array = rs.Cast<string>().ToArray();

            //string[] P1 = new string[rs.Count];
            //for (int i1 = 0; i1 < rs.Count; i1++)
            //{
            //    rs.CopyTo(P1, i1);
            //}

            return Json(new { student = array,size = rs.Count }, JsonRequestBehavior.AllowGet);
        }

        // Method to retive language data for table

        public ActionResult getProficiency(string stuId)
        {

            string qryProficiency = "select * from dbo.languageProficiency where RegistrationNum= @r1";
            List<object> parameterListLanguage = new List<object>();
            parameterListLanguage.Add(new SqlParameter("@r1", stuId));
            object[] r1 = parameterListLanguage.ToArray();
            IList<int> rs = db.Database.SqlQuery<int>(qryProficiency, r1).ToList<int>();

            Array arr = rs.Cast<int>().ToArray();

            return Json(new { chartData = arr, size = rs.Count }, JsonRequestBehavior.AllowGet);
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
