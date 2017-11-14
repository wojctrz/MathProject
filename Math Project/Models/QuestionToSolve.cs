using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathProject.Models
{
    public class QuestionToSolve : Question
    {
        public bool WasAnswered { get; set; }
        public bool WasCorrectlyAnswered { get; set; }
        public QuestionToSolve(Question question)
        {
            WasAnswered = false;
            WasCorrectlyAnswered = false;
            this.ID = question.ID;
            this.Category = question.Category;
            this.Content = question.Content;
            this.CorrectAnswer = question.CorrectAnswer;
            this.Hints = question.Hints;
        }
        public QuestionToSolve()
        {

        }
    }
}
