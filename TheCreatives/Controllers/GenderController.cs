using System.Linq;
using System.Web.Mvc;
using ModelsLogic;
using System.Data;
using BusinessLogic;

namespace TheCreatives.Controllers
{
    public class GenderController : Controller
    {
        private IRepository<Gender> interfaceobj;

        public GenderController()
        {
            this.interfaceobj = new Repository<Gender>();
        }


        //Index: Gender
        public ActionResult Index()
        {
            return View(from gender in interfaceobj.GetModel() select gender);
       
        }


        //GET: Gender/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //POST:Gender/AddGender
        [HttpPost]
        public ActionResult Create(Gender gender)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.InsertModel(gender);
                    interfaceobj.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try Again if the problem persists see your system administrator.");
            }

            return View(gender);
        }


        // Details/Id
        public ActionResult Details(int id)
        {
            Gender gender = interfaceobj.GetModelByID(id);

            return View(gender);
        }


        //GET:Gender/Edit/5
        public ActionResult Edit(int id)
        {
            Gender gender = interfaceobj.GetModelByID(id);
            return View(gender);
        }


        // POST: Gender/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Gender gender)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(gender);
                    interfaceobj.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            return View(gender);
        }


        // GET: Gender/Delete/5
        public ActionResult Delete(int id)
        {
            Gender gender = interfaceobj.GetModelByID(id);

            return View(gender);

        }


        // POST: Gender/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, FormCollection gender)
        {
            try
            {
                interfaceobj.DeleteModel(id);
                interfaceobj.Save();

                return RedirectToAction("Index");
            }
            catch(DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(gender);
            }
        }
    }
}
