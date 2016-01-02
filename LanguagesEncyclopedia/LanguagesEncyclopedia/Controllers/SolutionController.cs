using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguagesEncyclopedia.Models;


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


        public List<SelectListItem> GetLanguageTasks()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var obj in db.LanguageTasks)
            {
                SelectListItem item = new SelectListItem();

                item.Value = obj.LanguageTaskID.ToString();
                item.Text = obj.Task.Name;

                items.Add(item);
            }

            return items;

        }

        //
        // GET: /Solution/Create

        public ActionResult Create()
        {
            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID");

            List<SelectListItem> items = GetLanguageTasks();

            MergedSolutionModel model = new MergedSolutionModel(items);

            return View(model);
        }

        //
        // POST: /Solution/Create

        [HttpPost]
        public ActionResult Create(MergedSolutionModel model)
        {
            if (ModelState.IsValid)
            {
                db.Solutions.AddObject(model.solution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID", model.solution.LanguageTaskID);

            List<SelectListItem> items = GetLanguageTasks();

            return View(model);
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

            List<SelectListItem> items = GetLanguageTasks();

            MergedSolutionModel model = new MergedSolutionModel(items);

            model.solution = solution;

            return View(model);
        }

        //
        // POST: /Solution/Edit/5

        [HttpPost]
        public ActionResult Edit(MergedSolutionModel model)
        {
            if (ModelState.IsValid)
            {
                db.Solutions.Attach(model.solution);
                db.ObjectStateManager.ChangeObjectState(model.solution, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageTaskID = new SelectList(db.LanguageTasks, "LanguageTaskID", "LanguageTaskID", model.solution.LanguageTaskID);

            List<SelectListItem> items = GetLanguageTasks();

            return View(model);
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