using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IngWebProject.Models;

namespace IngWebProject.Controllers
{
    public class MasteriesController : Controller
    {
        private IngWebProjectEntities1 db = new IngWebProjectEntities1();

        // GET: Masteries
        public ActionResult Index()
        {
            List<string> options = GetFilterOptions();
            ViewBag.FilterOptions = new SelectList(options, "Seleccionar");
            var masteries = db.Masteries.Include(m => m.Question).Include(m => m.User);
            return View(masteries.ToList());
        }

        public List<string> GetFilterOptions()
        {
            List<string> options = new List<string>();
            options.Add("Easy");
            options.Add("Medium");
            options.Add("Hard");
            options.Add("Seleccionar");
            return options;
        }
        public ActionResult CutDownResults(string FilterOptions)
        {
            List<string> options = GetFilterOptions();
            ViewBag.FilterOptions = new SelectList(options, "Seleccionar");
            if (FilterOptions.Equals("Seleccionar"))
            {
                return View("Index", db.Masteries.ToList());
            }
            else
            {
                if (FilterOptions.Equals("Easy"))
                {
                    return View("Index", db.Masteries.Where(x => x.DifficultyLevel.Equals("Easy")).ToList());
                }
                else if (FilterOptions.Equals("Medium"))
                {
                    return View("Index", db.Masteries.Where(x => x.DifficultyLevel.Equals("Medium")).ToList());
                }
                else if (FilterOptions.Equals("Hard"))
                {
                    return View("Index", db.Masteries.Where(x => x.DifficultyLevel.Equals("Hard")).ToList());
                }
                else
                {
                    return View("Index", db.Masteries.ToList());
                }
            }
        }

        // GET: Masteries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mastery mastery = db.Masteries.Find(id);
            if (mastery == null)
            {
                return HttpNotFound();
            }
            return View(mastery);
        }

        // GET: Masteries/Create
        public ActionResult Create()
        {
            ViewBag.QuestionsID = new SelectList(db.Questions, "QuestionsID", "QuestionText");
            ViewBag.UsersID = new SelectList(db.Users, "UsersID", "Username");
            return View();
        }

        // POST: Masteries/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasteryID,UsersID,QuestionsID,TotalTries,CorrectTries,DifficultyLevel,DifficultyPercentage")] Mastery mastery)
        {
            if (ModelState.IsValid)
            {
                db.Masteries.Add(mastery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionsID = new SelectList(db.Questions, "QuestionsID", "QuestionText", mastery.QuestionsID);
            ViewBag.UsersID = new SelectList(db.Users, "UsersID", "Username", mastery.UsersID);
            return View(mastery);
        }

        // GET: Masteries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mastery mastery = db.Masteries.Find(id);
            if (mastery == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionsID = new SelectList(db.Questions, "QuestionsID", "QuestionText", mastery.QuestionsID);
            ViewBag.UsersID = new SelectList(db.Users, "UsersID", "Username", mastery.UsersID);
            return View(mastery);
        }

        // POST: Masteries/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasteryID,UsersID,QuestionsID,TotalTries,CorrectTries,DifficultyLevel,DifficultyPercentage")] Mastery mastery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mastery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionsID = new SelectList(db.Questions, "QuestionsID", "QuestionText", mastery.QuestionsID);
            ViewBag.UsersID = new SelectList(db.Users, "UsersID", "Username", mastery.UsersID);
            return View(mastery);
        }

        // GET: Masteries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mastery mastery = db.Masteries.Find(id);
            if (mastery == null)
            {
                return HttpNotFound();
            }
            return View(mastery);
        }

        // POST: Masteries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mastery mastery = db.Masteries.Find(id);
            db.Masteries.Remove(mastery);
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
    }
}
