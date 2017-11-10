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
                new Question{Category = Categories.FunkcjaKwadratowa, Content="f(x) = x^2 + 2x - 1. Oblicz deltę", CorrectAnswer = 8 },
                new Question{Category = Categories.Trygonometria, Content="ile wynosi sinus 0?", CorrectAnswer = 0 },
                new Question{Category = Categories.FunkcjaKwadratowa, Content="Ile możliwych rozwiązań ma równanie 3x^2 + 2x + 12 = 0 ?", CorrectAnswer = 0 },
                new Question{Category = Categories.Planimetria, Content="obwód trójkąta prostokątnego o przyprostokątnych równych 3 i 4 wynosi", CorrectAnswer = 12 },
                new Question{Category = Categories.Stereometria, Content="objętość graniastosłupa prawidłowego czworokątnego o polu podstawy 16 i wysokości trzy razy dłuższej od krawędzi podstawy wynosi", CorrectAnswer = 192 }
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
                new Hint{QuestionID = qlist.ElementAt(1).ID, Content = "test", CorrectAnswer = 1900}
            };
            foreach (Hint h in hints)
            {
                context.Hint.Add(h);
            }
            context.SaveChanges();
        }
    }
}
