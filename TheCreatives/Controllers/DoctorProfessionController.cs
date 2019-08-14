using System.Web.Mvc;
using DataAccessLogic;
using System.Data;
using System.Linq;
using ModelsLogic;
using BusinessLogic;

namespace TheCreatives.Controllers
{
    public class DoctorProfessionController : Controller
    {
        private IRepository<DoctorProfesion> interfaceobj;
       
        public DoctorProfessionController()
        {
            this.interfaceobj = new Repository<DoctorProfesion>();
        }


        //GET: DoctorProfessions
        public ActionResult Index()
        {
            return View(from professions in interfaceobj.GetModel() select professions);
        }


        //GET: /DoctorProfessions/AddDoctorProfession
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(DoctorProfesion doctorProfesion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.InsertModel(doctorProfesion);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try Againand if the problem persists see your system administrator.");
            }

            return View(doctorProfesion);
        }
    

        // Details/Id
        public ActionResult Details(int id)
        {
            DoctorProfesion profesion = interfaceobj.GetModelByID(id);

            return View(profesion);
        }

        //GET:/DoctorProfession/Edit/5
        public ActionResult Edit(int id)
        {
            DoctorProfesion docProfession = interfaceobj.GetModelByID(id);
            return View(docProfession);
        }


        // POST: /DoctorProfession/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorProfesion doctorProfesion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(doctorProfesion);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            return View(doctorProfesion);
        }


        // GET: /DoctorProfession/Delete/5
        public ActionResult Delete(int id)
        {
            DoctorProfesion docProf = interfaceobj.GetModelByID(id);

            return View(docProf);

        }


        // POST: Gender/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, FormCollection docProf)
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
                return View(docProf);
            }
        }

    }
}