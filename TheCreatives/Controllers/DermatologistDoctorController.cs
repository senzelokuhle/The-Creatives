using System.Web.Mvc;
using DataAccessLogic;
using ModelsLogic;
using System.Linq;
using System.Data;
using BusinessLogic;

namespace TheCreatives.Controllers
{
    public class DermatologistDoctorController : Controller
    {
        private IRepository<DermatologistsDoctor> interfaceobj;
        private EntityContext db = new EntityContext();

        public DermatologistDoctorController()
        {
            this.interfaceobj = new Repository<DermatologistsDoctor>();
        }


        //Index: DermatologistsDoctor
        public ActionResult Index()
        {
            return View(from dermDoc in interfaceobj.GetModel() select dermDoc);
        }


        //GET: /DermatologistsDoctor/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProfId = new SelectList(db.doctorprefesions, "ProfId", "ProfName"); //Learn to do this with repository
            return View();
        }


        //POST:/DermatologistsDoctor/Create
        [HttpPost]
        public ActionResult Create(DermatologistsDoctor dermatologist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.InsertModel(dermatologist);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try Again if the problem persists see your system administrator.");
            }

            ViewBag.ProfId = new SelectList(db.doctorprefesions, "ProfId", "ProfName", dermatologist.ProfId);
            return View(dermatologist);
        }


        // Details/Id
        public ActionResult Details(int id)
        {
            DermatologistsDoctor dermatologist = interfaceobj.GetModelByID(id);

            return View(dermatologist);
        }


        //GET:/Dermatologists/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ProfId = new SelectList(db.doctorprefesions, "ProfId", "ProfName"); //Learn to do this with repository
            DermatologistsDoctor dermagologist = interfaceobj.GetModelByID(id);
            return View(dermagologist);
        }


        // POST: /Dermatologists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DermatologistsDoctor dermatologist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(dermatologist);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            ViewBag.ProfId = new SelectList(db.doctorprefesions, "ProfId", "ProfName", dermatologist.ProfId);
            return View(dermatologist);
        }


        // GET: /Dermatologist/Delete/5
        public ActionResult Delete(int id)
        {
            DermatologistsDoctor dermatologist = interfaceobj.GetModelByID(id);

            return View(dermatologist);

        }


        // POST: Gender/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, FormCollection dermatologist)
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
                return View(dermatologist);
            }
        }
    }
}