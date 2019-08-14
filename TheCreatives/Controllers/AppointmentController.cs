using System.Web.Mvc;
using DataAccessLogic;
using BusinessLogic;
using ModelsLogic;
using System.Linq;
using System.Data;
using System.Net;
using SendGrid.Helpers.Mail;

namespace TheCreatives.Controllers
{
    public class AppointmentController : Controller
    {
        private IRepository<Appointment> interfaceObj;
        private EntityContext db = new EntityContext();

        public AppointmentController()
        {
            this.interfaceObj = new Repository<Appointment>();
        }
        public ActionResult Index()
        {
            return View(from dermaApp in interfaceObj.GetModel() select dermaApp);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Appointment dermApp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceObj.InsertModel(dermApp);
                    interfaceObj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try Again if the problem persists see your system administrator.");
            }

            return View(dermApp);
        }

        public JsonResult GetEvents()
        {
            var events = (from appointment in interfaceObj.GetModel() select appointment).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult SaveEvent(Appointment dermApp)
        {
            var status = false;

            using (EntityContext db = new EntityContext())
            {
                if (dermApp.EventID > 0)
                {
                    var v = db.dermatologistappointments.Where(a => a.EventID == dermApp.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = dermApp.Subject;
                        v.Start = dermApp.Start;
                        v.End = dermApp.End;
                        v.Email = dermApp.Email;
                        v.Cellphone = dermApp.Cellphone;
                        v.Description = dermApp.Description;
                        v.IsFullDay = dermApp.IsFullDay;
                        v.ThemeColor = dermApp.ThemeColor;
                    }
                }
                else
                {
                    db.dermatologistappointments.Add(dermApp);
                }

                var myMessage = new SendGridMessage
                {
                    From = new EmailAddress("no-reply@Code-Hub.com", "No-Reply")
                };

                myMessage.AddTo(dermApp.Email);
                string subject = "Registration Received";
                string body = ("Hi " + dermApp.Subject + " " + "\n" + "Here your booking details" + dermApp.Start + "." +
                "\n" + " Ensure not to share your password with anyone...  Have a great day." + "\n");
                myMessage.Subject = subject;
                myMessage.HtmlContent = body;
                var transportWeb = new SendGrid.SendGridClient("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");
                transportWeb.SendEmailAsync(myMessage);



                //string ToEmail, FromOrSenderEmail = ConfigurationManager.AppSettings["Email"], SubJect, Body, cc, Bcc;
                //ViewData["VS"] = "Start Date: " + dermApp.Start + "<br />" + "End Date: " +  dermApp.End + "<br /> " + "Description: " + dermApp.Description;
                //ToEmail = Request["Email"].ToString();
                //SubJect ="Clinic Appointment";

                //Body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Appointment/") + "AppointmentEmail" + ".cshtml") + ViewData["VS"].ToString();

                //cc = Request["Email"].ToString();
                //Bcc = Request["Email"].ToString();

                //WebMail.SmtpServer = "smtp.gmail.com";
                //WebMail.SmtpPort = 587; 
                //WebMail.SmtpUseDefaultCredentials = true;
                //WebMail.EnableSsl = true; 
                //WebMail.UserName = FromOrSenderEmail;
                //WebMail.Password = ConfigurationManager.AppSettings["Password"];
                //WebMail.From = FromOrSenderEmail;
                //WebMail.Send(to: ToEmail, subject: SubJect, body: Body, cc: cc, bcc: Bcc, isBodyHtml: true);

                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
            
        //public ActionResult AppointmentEmail()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (EntityContext db = new EntityContext())
            {
                var v = db.dermatologistappointments.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    db.dermatologistappointments.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ViewSchedule()
        {
            return View();
        }

    }
}

