using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguagesEncyclopedia.Views.Home;

namespace LanguagesEncyclopedia.Controllers
{
    public class SolutionController : Controller
    {
        private EncyclopediaEntities db = new EncyclopediaEntities();

        //
        // GET: /Solution/

        public ActionResult Index()
        {
            var solutions = db.Solutions.Include("LanguageTask");
            return View(solutions.ToList());
        }

        //
        // GET: /Solution/Details/5

        public ActionResult Details(int id = 0)
        {
            Solution solution = db.Solutions.Single(s => s.SolutionID == id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        //
        // GET: /Solution/Create

        public ActionResult Create()
        {
            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID");
            return View();
        }

        //
        // POST: /Solution/Create

        [HttpPost]
        public ActionResult Create(Solution solution)
        {
            if (ModelState.IsValid)
            {
                db.Solutions.AddObject(solution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID", solution.LanguageTaskID);
            return View(solution);
        }

        //
        // GET: /Solution/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Solution solution = db.Solutions.Single(s => s.SolutionID == id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID", solution.LanguageTaskID);
            return View(solution);
        }

        //
        // POST: /Solution/Edit/5

        [HttpPost]
        public ActionResult Edit(Solution solution)
        {
            if (ModelState.IsValid)
            {
                db.Solutions.Attach(solution);
                db.ObjectStateManager.ChangeObjectState(solution, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID", solution.LanguageTaskID);
            return View(solution);
        }

        //
        // GET: /Solution/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Solution solution = db.Solutions.Single(s => s.SolutionID == id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        //
        // POST: /Solution/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Solution solution = db.Solutions.Single(s => s.SolutionID == id);
            db.Solutions.DeleteObject(solution);
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