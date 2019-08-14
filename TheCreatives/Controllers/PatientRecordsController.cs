using ModelsLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheCreatives.Models;

namespace TheCreatives.Controllers
{
    public class PatientRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //Return list of patient records
        IEnumerable<PatientRecord> GetAllRecords()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var patientRecord = db.PatientRecords.Include(i => i.gender);
                return patientRecord.ToList<PatientRecord>();
            }
        }

        //View the list of records
        public ActionResult ViewAll()
        {

            return View(GetAllRecords());
        }
        public ActionResult Index()
        {
            var patientRecords = db.PatientRecords.Include(p => p.gender);
            return View(patientRecords.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "_Gender");
            return View();
        }

        // POST: PatientRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,PatientName,DateOfBirth,Address,HomeTelephone,CellPhone,IdentityNo,Employer,WorkNumber,EmergencyContact,NextOfKin,HomePhone,InsuranceName,PolicyNumber,InsuranceNumber,GenderId")] PatientRecord patientRecord)
        {
                if (ModelState.IsValid)
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        //Check if patients Id number is unique
                        var exist = db.PatientRecords.Where(i => i.IdentityNo == patientRecord.IdentityNo);
                        if (exist.Count() == 0)
                        {
                            patientRecord.Date = patientRecord.StartDate();
                            db.PatientRecords.Add(patientRecord);
                            db.SaveChanges();

                            TempData["SM"] = "Successfully Registerd!";
                            return RedirectToAction("Index"); /*Redirect to create patients default login details*/
                        }
                        else
                        {
                            TempData["UM"] = "Patient Already Exists. Check List of Patients!";
                            return RedirectToAction("Index");
                        }
                    }
                }

            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "_Gender", patientRecord.GenderId);
            return RedirectToAction("Index");


        }
        // GET: PatientRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "_Gender", patientRecord.Id);
            return View(patientRecord);
        }

        // POST: PatientRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PatientName,DateOfBirth,Address,HomeTelephone,CellPhone,IdentityNo,Employer,WorkNumber,EmergencyContact,NextOfKin,HomePhone,InsuranceName,PolicyNumber,InsuranceNumber,GenderId")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "_Gender", patientRecord.Id);
            return View(patientRecord);
        }

        // GET: PatientRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // POST: PatientRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            db.PatientRecords.Remove(patientRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Registration()
        {
            return View();
        }
    }
}