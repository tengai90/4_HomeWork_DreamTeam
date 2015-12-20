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
    public class LanguageTaskController : Controller
    {
        private EncyclopediaEntities db = new EncyclopediaEntities();

        //
        // GET: /LanguageTask/

        public ActionResult Index()
        {
            var languagetasks = db.LanguageTasks.Include("Language").Include("Task");
            return View(languagetasks.ToList());
        }

        //
        // GET: /LanguageTask/Details/5

        public ActionResult Details(int id = 0)
        {
            LanguageTask languagetask = db.LanguageTasks.Single(l => l.LanguageTaskID == id);
            if (languagetask == null)
            {
                return HttpNotFound();
            }
            return View(languagetask);
        }

        //
        // GET: /LanguageTask/Create

        public ActionResult Create()
        {
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Name");
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Name");
            return View();
        }

        //
        // POST: /LanguageTask/Create

        [HttpPost]
        public ActionResult Create(LanguageTask languagetask)
        {
            if (ModelState.IsValid)
            {
                db.LanguageTasks.AddObject(languagetask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Name", languagetask.LanguageID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Name", languagetask.TaskID);
            return View(languagetask);
        }

        //
        // GET: /LanguageTask/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LanguageTask languagetask = db.LanguageTasks.Single(l => l.LanguageTaskID == id);
            if (languagetask == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Name", languagetask.LanguageID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Name", languagetask.TaskID);
            return View(languagetask);
        }

        //
        // POST: /LanguageTask/Edit/5

        [HttpPost]
        public ActionResult Edit(LanguageTask languagetask)
        {
            if (ModelState.IsValid)
            {
                db.LanguageTasks.Attach(languagetask);
                db.ObjectStateManager.ChangeObjectState(languagetask, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Name", languagetask.LanguageID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Name", languagetask.TaskID);
            return View(languagetask);
        }

        //
        // GET: /LanguageTask/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LanguageTask languagetask = db.LanguageTasks.Single(l => l.LanguageTaskID == id);
            if (languagetask == null)
            {
                return HttpNotFound();
            }
            return View(languagetask);
        }

        //
        // POST: /LanguageTask/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LanguageTask languagetask = db.LanguageTasks.Single(l => l.LanguageTaskID == id);
            db.LanguageTasks.DeleteObject(languagetask);
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