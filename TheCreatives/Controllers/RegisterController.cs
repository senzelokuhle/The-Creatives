using BusinessLogic;
using ModelsLogic;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace TheCreatives.Controllers
{
    public class RegisterController : Controller
    {
        private IRepository<SiteUser> interfaceobj;

        public RegisterController()
        {
            this.interfaceobj = new Repository<SiteUser>();
        }


        //Patient Home View
        public ActionResult Home()
        {
            return View();
        }


        //Save Registration Details
        public JsonResult SaveData(SiteUser model)
        {

            model.IsValid = false;
            model.Password = model.setPassword();

            interfaceobj.InsertModel(model);
            interfaceobj.Save();

            BuildEmailTemplate(model.SiteUserId);

            var myMessage = new SendGridMessage
            {
                From = new EmailAddress("no-reply@Code-Hub.com", "No-Reply")
            };
            myMessage.AddTo(model.Email);
            string subject = "Registration Received";
            string body = ("Hi " + model.Username + " " + "\n" + "Here your booking details" + model.Password + "." +
            "\n" + " Ensure not to share your password with anyone...  Have a great day." + "\n");
            myMessage.Subject = subject;
            myMessage.HtmlContent = body;
            var transportWeb = new SendGrid.SendGridClient("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");
            transportWeb.SendEmailAsync(myMessage);



            TempData["SM"] = "Succesfully Registerd";

            return Json("Registration Successfull", JsonRequestBehavior.AllowGet);
        }


        //Patient Confirms email
        public ActionResult Confirm(int regId)
        {
            var regInfo = interfaceobj.GetModel().Where(a => a.SiteUserId == regId).FirstOrDefault();
            ViewBag.Username = regInfo.Email;
            ViewBag.Password = regInfo.Password;
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            SiteUser Data = interfaceobj.GetModel().Where(x => x.SiteUserId == regId).FirstOrDefault();
            Data.IsValid = true;
            interfaceobj.Save();
            var msg = "Your Email Is Verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckValidUser(SiteUser model)
        {
            string result = "Fail";
            var DataItem = interfaceobj.GetModel().Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();

            if (DataItem != null)
            {
                Session["UserID"] = DataItem.SiteUserId.ToString();
                Session["UserName"] = DataItem.Username.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Login Configuration
        public ActionResult AfterLogin()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("HomePage", "HomePage");
        }



        /*Email Configuarations - Needs Refactoring*/
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");

            var regInfo = interfaceobj.GetModel().Where(x => x.SiteUserId == regID).FirstOrDefault();
            var url = "http://localhost:30410/" + "Register/Confirm?regId=" + regID;

            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();

            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }
        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = ConfigurationManager.AppSettings["Email"].ToString(); /*configure this on web config*/
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;

            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SendEmail(mail);
        }
        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        [HttpGet]
        public ActionResult IndexView(SiteUser model)
        {
            var DataItem = interfaceobj.GetModel().Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();
            if(DataItem != null)
            {
                Session["Username"] = DataItem.SiteUserId.ToString();
            }
           
            return View(from siteuser in interfaceobj.GetModel().Where(x => x.Username == Session["Username"].ToString()) select siteuser);
        }



        //GET:/DoctorProfession/Edit/5
        public ActionResult Edit(int id)
        {
            SiteUser siteuser = interfaceobj.GetModelByID(id);
            return View(siteuser);
        }


        // POST: /DoctorProfession/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteUser siteuser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(siteuser);
                    interfaceobj.Save();
                    TempData["SM"] = "Updated Successfully!";
                    return View(siteuser);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            return View(siteuser);
        }
    }
}