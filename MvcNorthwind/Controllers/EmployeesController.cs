using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcNorthwind.Models;

namespace MvcNorthwind.Controllers
{
    public class EmployeesController : Controller
    {
        private NORTHWNDEntities db = new NORTHWNDEntities();

        //
        // GET: /Employees/

        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Employees2);
            return View(employees.ToList());
        }

        //
        // GET: /Employees/Details/5

        public ActionResult Details(int id = 0)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName");
            return View();
        }

        //
        // POST: /Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        //
        // GET: /Employees/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        //
        // GET: /Employees/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        //
        // POST: /Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}