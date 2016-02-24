using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sendin(string Email1 , string Id1,string client1)
        {
            
            Aspose.Email.Mail.SmtpClient client = new Aspose.Email.Mail.SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Username = "akayeshmantha@gmail.com";
            client.Password = "miriguwaparada2812213";
            client.Port = 587;
            client.SecurityOptions = Aspose.Email.SecurityOptions.SSLExplicit;
            Aspose.Email.Mail.MailMessage message = new Aspose.Email.Mail.MailMessage();
            message.From = new Aspose.Email.Mail.MailAddress("akayeshmantha@gmail.com");
            message.To.Add(new Aspose.Email.Mail.MailAddress("akayeshmantha@gmail.com"));
            message.Priority = Aspose.Email.Mail.MailPriority.High;
            
            int ids = 0;
            if (int.TryParse(Id1, out ids))
            {
                if (ids == -1)
                {
                    message.Subject = "U are a registered client Please add projects to the tool";
                   // message.HtmlBody = "<a href='http://localhost:55814/Projects/Create'>Go to register </a>";
                    message.HtmlBody=("<a href='http://localhost:55814/Projects/Create?client="+client1+"'>Go to register </a>");
                    Debug.Write("aissaaa"+message.HtmlBody);
                }
                else if (ids == 0)
                {
                    message.Subject = "U have not registered to the tool pelase register first to add Projects to the tool";
                    message.HtmlBody = "<a href='http://localhost:55814/Clients/Create'> Go to register </a>";

                }
            }
            else
            {
                message.Subject = "Giving the User Name";
                message.Body = "Your Registration No" + Id1;

            }

            client.Send(message);

            return RedirectToAction("Login","Register");
        }
    }
}