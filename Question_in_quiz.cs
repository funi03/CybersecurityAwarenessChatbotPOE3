using System;
using System.Collections.Generic;
using System.Linq;

namespace Cybersecurity_Awareness_Chatbot_Part2
{
  
       
        public class Question_in_quiz
        {
            // ============ PROPERTIES ============

            
            public string Text { get; set; }

           
            public string CorrectAnswer { get; set; }

         
            public List<string> WrongAnswers { get; set; }

         
            public string Explanation { get; set; }

           
            public string Category { get; set; }

            public int Difficulty { get; set; }

           
            public int Id { get; set; }

            
            public string Hint { get; set; }

            // ============ CONSTRUCTORS ============

            public Question_in_quiz()
            {
                WrongAnswers = new List<string>();
                Difficulty = 1;
                Category = "General";
            }

            public Question_in_quiz(string text, string correctAnswer, List<string> wrongAnswers)
                : this()
            {
                Text = text;
                CorrectAnswer = correctAnswer;
                WrongAnswers = wrongAnswers ?? new List<string>();
            }

            public Question_in_quiz(string text, string correctAnswer, List<string> wrongAnswers,
                                   string explanation, string category = "General", int difficulty = 1)
                : this(text, correctAnswer, wrongAnswers)
            {
                Explanation = explanation;
                Category = category;
                Difficulty = difficulty;
            }

            // ============ PUBLIC METHODS ============

            
            public List<string> GetAllOptionsShuffled()
            {
                var allOptions = new List<string>(WrongAnswers);
                allOptions.Add(CorrectAnswer);

                // Shuffle the list
                var random = new Random();
                for (int i = allOptions.Count - 1; i > 0; i--)
                {
                    int j = random.Next(i + 1);
                    (allOptions[i], allOptions[j]) = (allOptions[j], allOptions[i]);
                }

                return allOptions;
            }

           
            public bool IsCorrect(string answer)
            {
                if (string.IsNullOrEmpty(answer) || string.IsNullOrEmpty(CorrectAnswer))
                    return false;

                return answer.Trim().Equals(CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
            }

            
            public int GetCorrectAnswerIndex(List<string> shuffledOptions)
            {
                return shuffledOptions.FindIndex(opt =>
                    opt.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase));
            }

            
            public string GetDifficultyString()
            {
                switch (Difficulty)
                {
                    case 1: return "🟢 Easy";
                    case 2: return "🟡 Medium";
                    case 3: return "🔴 Hard";
                    default: return "🟡 Medium";
                }
            }

            public override string ToString()
            {
                return $"[{Category}] {Text} ({GetDifficultyString()})";
            }

          
        }
    }

