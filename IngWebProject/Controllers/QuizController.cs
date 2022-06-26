using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using IngWebProject.Models;
using IngWebProject.Scripts;

namespace IngWebProject.Controllers
{
    public class QuizController : Controller
    {
        private IWContext db = new IWContext();
        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuizUse()
        {
            List<string> diffOptions = new List<string>();
            diffOptions.Add("Select");
            diffOptions.Add("Easy");
            diffOptions.Add("Medium");
            diffOptions.Add("Hard");
            diffOptions.Add("Auto");
            ViewBag.DiffSelect = new SelectList(diffOptions);
            return View();
        }

        public ActionResult StartQuiz(string ObjActual)
        {
            SelectDifficulty(ObjActual);
            if (!Utility.quizDiff.Equals("Auto"))
            {
                int firstQID = Utility.ChooseNewQuestion();
                var list = db.Options.Where(x => x.QuestionsID == firstQID).ToList();
                Option model = list[0];
                ViewBag.OptionsID = new SelectList(list, "OptionsID", "OptionText");
                return PartialView("QuestionPartial", model);
            }
            else
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                List<Score> lastScore = db.Scores.Where(x => x.UsersID ==userID).ToList();
                List<Mastery> userMasteries = db.Masteries.Where(x=>x.UsersID == userID).ToList();
                List<double> limits = Utility.calculateLastValue(lastScore, userMasteries);
                double upperLim = limits[0];
                double lowerLim = limits[1];
                List<Mastery> toSelect = db.Masteries.Where(x => x.DifficultyPercentage > lowerLim & x.DifficultyPercentage < upperLim &x.UsersID==userID).ToList();
                int qID = Utility.ChooseAutoQuestion(toSelect);
                var opList = db.Options.Where(x => x.QuestionsID == qID).ToList();
                Option model = opList[0];
                ViewBag.OptionsID = new SelectList(opList, "OptionsID", "OptionText");
                return PartialView("QuestionPartial", model);
            }
        }

        public ActionResult NextQuestion(int OptionsID)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            UpdateMastery(OptionsID);
            if (Utility.nQues != 9)
            {
                if (!Utility.quizDiff.Equals("Auto"))
                {
                    UpdateQuizState(OptionsID);
                    int firstQID = Utility.ChooseNewQuestion();
                    var list = db.Options.Where(x => x.QuestionsID == firstQID).ToList();
                    Option model = list[0];
                    ViewBag.OptionsID = new SelectList(list, "OptionsID", "OptionText");
                    return View("Questions", model);
                }
                else
                {
                    UpdateQuizState(OptionsID);
                    List<Score> lastScore = db.Scores.Where(x => x.UsersID == userID).ToList();
                    List<Mastery> userMasteries = db.Masteries.Where(x => x.UsersID == userID).ToList();
                    List<double> limits = Utility.calculateLastValue(lastScore, userMasteries);
                    double upperLim = limits[0];
                    double lowerLim = limits[1];
                    List<Mastery> toSelect = db.Masteries.Where(x => x.DifficultyPercentage > lowerLim & x.DifficultyPercentage < upperLim & x.UsersID == userID).ToList();
                    int qID = Utility.ChooseAutoQuestion(toSelect);
                    var opList = db.Options.Where(x => x.QuestionsID == qID).ToList();
                    Option model = opList[0];
                    ViewBag.OptionsID = new SelectList(opList, "OptionsID", "OptionText");
                    return View("Questions", model);
                }                
            }
            else
            {
                Score s = new Score();
                s.MODE = Utility.quizDiff;
                s.Points = (Utility.correctAnswers * 100) * 0.9 + Utility.combo * 10;
                s.Combo = Utility.maxCombo;
                s.UsersID = userID;
                db.Scores.Add(s);
                db.SaveChanges();
                Utility.resetValues();
                return View("../Scores/Index", db.Scores.ToList());
            }
        }

        public void SelectDifficulty(string diff)
        {
            switch (diff)
            {
                case "Easy":
                    Utility.quizDiff = diff;
                    Utility.quesToChooseFrom = db.Questions.Where(x => x.InitialDifficulty.Equals(diff)).ToList();
                    break;
                case "Medium":
                    Utility.quizDiff = diff;
                    Utility.quesToChooseFrom = db.Questions.Where(x => x.InitialDifficulty.Equals(diff)).ToList();
                    break;
                case "Hard":
                    Utility.quizDiff = diff;
                    Utility.quesToChooseFrom = db.Questions.Where(x => x.InitialDifficulty.Equals(diff)).ToList();
                    break;
                case "Auto":
                    if (db.Masteries.ToList().Count < 10)
                    {
                        Utility.quizDiff = diff;
                        Utility.quesToChooseFrom = db.Questions.Where(x => x.InitialDifficulty.Equals(diff)).ToList();
                    }
                    else
                    {
                        Utility.quizDiff = diff;
                        Utility.quesToChooseFrom = db.Questions.ToList();
                    }
                    break;
            }
        }

        /// <summary>
        /// Actualiza el estado del Quiz
        /// </summary>
        /// <param name="id">ID de la respuesta escogida</param>
        public void UpdateQuizState(int id)
        {
            
            Option currentOption = db.Options.Where(x => x.OptionsID == id).ToList()[0];
            Question q = currentOption.Question;
            Utility.answeredQuestions.Add(q);
            Utility.nQues++;

            if (currentOption.isCorrect)
            {
                Utility.combo++;
                Utility.correctAnswers++;
                Utility.negCombo--;
            }
            else
            {
                Utility.maxCombo = Utility.combo;
                Utility.combo = 0;
                Utility.negCombo++;
            }
        }

        /// <summary>
        /// Actualiza la maestria, llama a dos funciones, una actualiza una maestria existente y otra
        /// crea una entrada de maestria en la BD.
        /// </summary>
        /// <param name="id">ID de la respuesta escogida</param>
        public void UpdateMastery(int id)
        {
            Option currentOption = db.Options.Where(x => x.OptionsID == id).ToList()[0];
            int userID = Convert.ToInt32(Session["UserID"]);
            var list = db.Masteries.Where(x => x.QuestionsID == currentOption.QuestionsID & x.UsersID==userID).ToList();
            if (list.Count == 0)
            {
                createMastery(currentOption);
            }
            else
            {
                updateExistingMastery(currentOption, list[0]);
            }
            db.SaveChanges();
        }

        public void updateExistingMastery(Option currentOp, Mastery currentM)
        {
            currentM.TotalTries++;
            if (currentOp.isCorrect)
            {
                currentM.CorrectTries++;
            }
            currentM.DifficultyPercentage = (Convert.ToDouble(currentM.CorrectTries) / Convert.ToDouble(currentM.TotalTries)) * 100f;
            currentM.DifficultyLevel = DetermineMasteryDifficulty(currentM.DifficultyPercentage);
            db.Entry(currentM).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void createMastery(Option current)
        {
            Mastery m = new Mastery();
            int userID = Convert.ToInt32(Session["UserID"]);
            m.UsersID = userID;
            m.QuestionsID = current.QuestionsID;
            m.TotalTries = 1;
            if (current.isCorrect)
            {
                m.CorrectTries = 1;
            }
            else
            {
                m.CorrectTries = 0;
            }
            m.DifficultyPercentage = (m.CorrectTries / m.TotalTries) * 100f;
            m.DifficultyLevel = DetermineMasteryDifficulty(m.DifficultyPercentage);
            db.Masteries.Add(m);
        }

        public string DetermineMasteryDifficulty(double percentage)
        {
            if (percentage < 30)
            {
                return "Hard";
            }
            else if (percentage < 60)
            {
                return "Medium";
            }
            else{
                return "Easy";
            }
        }

        
    }
}