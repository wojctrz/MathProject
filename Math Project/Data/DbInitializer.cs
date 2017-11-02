using MathProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MathProjectContext context)
        {
            context.Database.EnsureCreated();

            // Look for any questions
            if (context.Question.Any())
            {
                return;   // DB has been seeded
            }

            var questions = new Question[]
            {
                new Question{Category = Categories.FunkcjaKwadratowa, Content="HALO HALO test test", CorrectAnswer = 3.5 },
                new Question{Category = Categories.Planimetria, Content="HALO ", CorrectAnswer = 322 }
            };
            foreach (Question q in questions)
            {
                context.Question.Add(q);
            }
            context.SaveChanges();

            var qlist = context.Question.ToList();
            var hints = new Hint[]
            {               
                new Hint{QuestionID = qlist.ElementAt(0).ID, Content = "text", CorrectAnswer = 11.2},  //qlist.ElementAt(0).ID zwraca id pierwszego pytania (bo nie zawsze jest 1 bo ten sql jakiś dziwny jest)
                new Hint{QuestionID = qlist.ElementAt(1).ID, Content = "xdxd", CorrectAnswer = 1900}
            };
            foreach (Hint h in hints)
            {
                context.Hint.Add(h);
            }
            context.SaveChanges();
        }
    }
}
