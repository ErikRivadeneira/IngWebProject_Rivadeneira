using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IngWebProject.Models;

namespace IngWebProject.Scripts
{
    public static class Utility
    {
        public static string quizDiff="None";
        public static List<Question> quesToChooseFrom;
        public static List<Question> answeredQuestions = new List<Question>();
        public static int nQues = 0;
        public static int combo = 0;
        public static int maxCombo = 0;
        public static int correctAnswers = 0;
        public static int negCombo = 0;
        public static string autoDiff = "None";
        
        public static int ChooseNewQuestion()
        {
            bool done;
            var randomGenerator = new Random();
            int id = randomGenerator.Next(0, quesToChooseFrom.Count);
            do {
                done = false;
                id = randomGenerator.Next(0, quesToChooseFrom.Count);
                if (answeredQuestions.Count != 0)
                {
                    foreach (Question item in answeredQuestions)
                    {
                        if (item.QuestionsID == id)
                        {
                            done = true;
                            break;
                        }
                    }
                }
            } while (done);
            quesToChooseFrom.RemoveAt(id);
            return id;
        }

        public static int ChooseAutoQuestion(List<Mastery> masteries)
        {
            var rand = new Random();
            int id = rand.Next(0, masteries.Count);
            return masteries[id].QuestionsID;
        }

        public static List<double> calculateLastValue(List<Score> last, List<Mastery> masteries)
        {
            double value = 0f;
            foreach (Mastery m in masteries)
            {
                value += m.DifficultyPercentage;
            }
            double sAvg = 0f;
            foreach(Score s in last)
            {
                sAvg += (s.Points / 10f);
            }
            sAvg = sAvg / last.Count;
            double prom = value / masteries.Count;
            value = ((prom + (sAvg / 10)) / 2) - (negCombo * 10);
            value = 100 - value;
            autoDiff = DetermineAutoDifficulty(value);
            List<double> limits = new List<double>();
            limits.Add(value + 10);
            limits.Add(value - 10);
            return limits;
        }

        public static void resetValues()
        {
            quizDiff = "None";
            nQues = 0;
            quesToChooseFrom.Clear();
            answeredQuestions.Clear();
            maxCombo = 0;
            combo = 0;
            correctAnswers = 0;
            negCombo = 0;
            autoDiff = "None";
        }

        public static string DetermineAutoDifficulty(double percentage)
        {
            if (percentage < 35)
            {
                return "Hard";
            }
            else if (percentage < 65)
            {
                return "Medium";
            }
            else
            {
                return "Easy";
            }
        }
    }
}