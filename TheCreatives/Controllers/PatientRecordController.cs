using System.Linq;
using System.Web.Mvc;
using ModelsLogic;
using DataAccessLogic;
using System.Data;
using BusinessLogic;

namespace TheCreatives.Controllers
{
    public class PatientRecordController : Controller
    {

        private IRepository<PatientRecord> interfaceobj;
        private EntityContext db = new EntityContext();

        public PatientRecordController()
        {
            this.interfaceobj = new Repository<PatientRecord>();
        }


        //Index: PatientRecord
        public ActionResult Index()
        {
            return View();
        }

        //GET: GetPatients
        public ActionResult GetPatients()
        {
            return View(from patients in interfaceobj.GetModel() select patients);
        }

        //GET: Patients/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.genders, "GenderId", "_Gender");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PatientRecord patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Check for patient existance
                    var exist = interfaceobj.GetModel().Where(i => i.IdentityNo == patient.IdentityNo);
                   
                    //If Patient doesn't exist
                    if (exist.Count() == 0)
                    {
                        patient.Date = patient.StartDate();
                        interfaceobj.InsertModel(patient);
                        interfaceobj.Save();

                        TempData["SM"] = "Successfully Registerd!";

                        //Redirect to create patients login details
                        return RedirectToAction("SignUp","Register");
                    }
                    else
                    {
                        //If patients record exist
                        TempData["UM"] = "Patient Id No. Already Exists. Check List of Patients!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try Again and if the problem persists see your system administrator.");
            }
            TempData["UM"] = "Unable to save. Try Again and if the problem persists see your system administrator!";

          
            ViewBag.GenderId = new SelectList(db.genders, "GenderId", "_Gender", patient.GenderId);
            return RedirectToAction("Index");
        }

        //GET:PatientRecord/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AuthorId = new SelectList(db.genders, "GenderId", "_Gender");
            PatientRecord patient = interfaceobj.GetModelByID(id);
            return View(patient);
        }

        // POST: PatientRecord/Edit/5
        [HttpPost]
        public ActionResult Edit(PatientRecord patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(patient);
                    interfaceobj.Save();
                    return RedirectToAction("GetPatients");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.AuthorId = new SelectList(db.genders, "GenderId", "_Gender", patient.GenderId);
            return View(patient);
        }



        // GET: PatientRecord/Delete/5
        public ActionResult Delete(int id)
        {
            PatientRecord patientRecord = interfaceobj.GetModelByID(id);

            return View(patientRecord);

        }


        // POST: PatientRecord/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, FormCollection patientRecord)
        {
            try
            {
                interfaceobj.DeleteModel(id);
                interfaceobj.Save();

                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(patientRecord);
            }
        }
        public ActionResult Registration()
        {

            return View();
        }
    }
}