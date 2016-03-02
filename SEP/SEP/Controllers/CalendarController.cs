using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using System.Data.Entity;

using System.Data.Linq;
using SEP.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SEP.Controllers
{
    public class CalendarController : Controller
    {
        private DB2 db = new DB2();

        public ActionResult Index()
        {

            if ((string)Session["Position"] == "student")
            {
                ViewBag.StMsg = "Click on the Calendar to add a Request for a Supervisor Meeting";
            }
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;

            scheduler.InitialDate = new DateTime();
            scheduler.Config.multi_day = true;
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public Lecturer GetStudentData()
        {
            string studentId = ((string)Session["id"]);
            Group supervisorName = (from Y in db.StudentGroupeLists
                          join B in db.Groups on Y.GroupNo equals B.GroupID
                          where (Y.StuId == studentId)
                          select B).FirstOrDefault<Group>();

            Lecturer lecturerId = (from m in db.Lecturers
                          where m.Name == supervisorName.Supervisor
                          select m).FirstOrDefault<Lecturer>();
            return lecturerId;

        }
    
        public ContentResult Data()
        {
            if ((string)Session["Position"] == "student")
            {
                             
                var supervisor = GetStudentData();
                var supervisorData = from m in db.Event
                        where m.LecturerId == supervisor.LecturerId
                        select m;

                var data = new SchedulerAjaxData(supervisorData);
                return (ContentResult)data;
            }
            else
            {
                string lecturerId = ((string)Session["id"]);
                var appoinmentData = from m in db.Event
                         where m.LecturerId == lecturerId
                         select m;

                var data = new SchedulerAjaxData(appoinmentData);
                return (ContentResult)data;

            }
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
            var data = new CalendarDataContext();

            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        //action.TargetId = changedEvent.id;
                        if ((string)Session["Position"] == "student")
                        {
                            var supervisorId = GetStudentData();
                            changedEvent.LecturerId = supervisorId.LecturerId.ToString();

                            string time = DateTime.Now.ToString("HH:mm:ss tt");
                            string query = "insert into  dbo.Requests(Name,Request,Status,Loaded) values(@a1,@a2,@a3,@a4)";
                            List<object> parameterList = new List<object>();
                            parameterList.Add(new SqlParameter("@a1", changedEvent.LecturerId));
                            parameterList.Add(new SqlParameter("@a2", Session["UserName"] + "Requests for a meeting on" + changedEvent.start_date + "CUT" + Session["Avatar"] + "CUT" + time + "CUT" + changedEvent.id + "CUTAppoinment"));
                            parameterList.Add(new SqlParameter("@a3", 2));
                            parameterList.Add(new SqlParameter("@a4", 2));
                            object[] parameters123 = parameterList.ToArray();
                            int rs = db.Database.ExecuteSqlCommand(query, parameters123);

                            data.Events.InsertOnSubmit(changedEvent);
                            break;
                        }
                        else
                        {
                            string lid = (string)Session["id"];
                            changedEvent.LecturerId = lid;
                            data.Events.InsertOnSubmit(changedEvent);
                            break;

                        }
                    //{
                    //if (changedEvent.start_date < DateTime.Today)
                    //{
                    //    TempData["MsgDate"] = "Select valid Appoinment Date";
                    //    RedirectToAction("Index");
                    //}


                    case DataActionTypes.Delete:
                        //do delete
                        if ((string)Session["Position"] == "student")
                        {
                            TempData["MsgDlt"] = "You Dont have permission to delete the Event!";
                            //  RedirectToAction("Index");
                            break;
                        }
                        else
                        {
                            changedEvent = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                            data.Events.DeleteOnSubmit(changedEvent);
                            break;
                            //    return (ContentResult)new AjaxSaveResponse(action);

                        }
                    default:
                        //do update
                        if ((string)Session["Position"] == "student")
                        {
                            break;
                        }
                        else
                        {
                            var eventToUpdate = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                            DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });//update all properties, except for id
                            break;

                            //   return (ContentResult)new AjaxSaveResponse(action);
                        }
                }

                data.SubmitChanges();
                action.TargetId = changedEvent.id;
            }
            catch (Exception e)
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

