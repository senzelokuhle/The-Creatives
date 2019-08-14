using System.Web.Mvc;
using DataAccessLogic;
using ModelsLogic;
using System.Linq;
using System.Data;
using BusinessLogic;

namespace TheCreatives.Controllers
{
    public class DermatologyServiceController : Controller
    {
        private IRepository<DermatologyService> interfaceobj;
     
        public DermatologyServiceController()
        {
            this.interfaceobj = new Repository<DermatologyService>();
        }


        //Index:DermatologyService
        public ActionResult Index()
        {
            return View(from dermServicie in interfaceobj.GetModel() select dermServicie);
        }


        //GET: /DermatologyService/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //POST:/DermatologyService/Create
        [HttpPost]
        public ActionResult Create(DermatologyService dermatoServ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.InsertModel(dermatoServ);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try Againand if the problem persists see your system administrator.");
            }

            return View(dermatoServ);
        }

        // Details/Id
        public ActionResult Details(int id)
        {
            DermatologyService dermarServi = interfaceobj.GetModelByID(id);

            return View(dermarServi);
        }


        //GET:/DermatologyService/Edit/5
        public ActionResult Edit(int id)
        {
            DermatologyService dermarServi = interfaceobj.GetModelByID(id);
            return View(dermarServi);
        }


        // POST: /DermatologyService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DermatologyService dermatoServ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interfaceobj.UpdateModel(dermatoServ);
                    interfaceobj.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            return View(dermatoServ);
        }


        // GET: /DermatologyService/Delete/5
        public ActionResult Delete(int id)
        {
            DermatologyService dermatoServender = interfaceobj.GetModelByID(id);

            return View(dermatoServender);
        }

        // POST: /DermatologyService/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection dermatoServender)
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
                return View(dermatoServender);
            }
        }
    }
}