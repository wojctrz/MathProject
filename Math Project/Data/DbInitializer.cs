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
                new Question{Category = Categories.Trygonometria, Content="Oblicz, ile wynosi tgα, wiedząc, że sinus tego kąta wynosi 0,8. Wynik podaj w postaci dziesiętnej, z dokładnością do części setnych.", CorrectAnswer = 1.33},
                new Question{Category = Categories.FunkcjaKwadratowa, Content="f(x) = x^2 + 2x - 3. Podaj mniejsze miejsce zerowe tego równania.", CorrectAnswer = -3 },
                new Question{Category = Categories.FunkcjaKwadratowa, Content="Ile możliwych rozwiązań ma równanie 3x^2 + 2x + 12 = 0 ?", CorrectAnswer = 0 },
                new Question{Category = Categories.Planimetria, Content="Obwód trójkąta prostokątnego o przyprostokątnych równych 3 i 4 wynosi", CorrectAnswer = 12 },
                new Question{Category = Categories.Stereometria, Content="Objętość graniastosłupa prawidłowego czworokątnego o polu podstawy 16 i wysokości trzy razy dłuższej od krawędzi podstawy wynosi", CorrectAnswer = 192 },
                new Question{Category = Categories.Stereometria, Content="Tangens kąta nachylenia przekątnej graniastosłupa prawidłowego czworokątnego do płaszczyzny podstawy wynosi 5√2/4, a długość tej przekątnej wynosi 2√33. Oblicz objętość tego graniastosłupa.", CorrectAnswer = 160 },
                new Question{Category = Categories.Planimetria, Content="Dane są trójkąty ABC oraz EBD. Punkt E leży na odcinku CB, a punkt D na odcinku AB. Odcinki AC i ED są równoległe. Wiedząc, że |AC|=10, |ED|=5 oraz |AD|=2, oblicz długość odcinka DB.", CorrectAnswer = 2 }
            };
            foreach (Question q in questions)
            {
                context.Question.Add(q);
            }
            context.SaveChanges();

            var qlist = context.Question.ToList();
            var hints = new Hint[]
            {
                new Hint{QuestionID = qlist.ElementAt(0).ID, Content = "Warto tutaj skorzystać ze wzoru tgα=sinα/cosα. Oblicz, ile wynosi cosα.", CorrectAnswer = 0.6},  //qlist.ElementAt(0).ID zwraca id pierwszego pytania (bo nie zawsze jest 1 bo ten sql jakiś dziwny jest)
                new Hint{QuestionID = qlist.ElementAt(1).ID, Content = "Zacznij od obliczenia delty.", CorrectAnswer = 16},
                new Hint{QuestionID = qlist.ElementAt(2).ID, Content = "Zacznij od obliczenia delty.", CorrectAnswer = -140},
                new Hint{QuestionID = qlist.ElementAt(3).ID, Content = "Korzystając z twierdzenia Pitagorasa, oblicz długość przeciwprostokątnej tego trójkąta.", CorrectAnswer = 5},
                new Hint{QuestionID = qlist.ElementAt(4).ID, Content = "Ile wynosi wysokość tego graniastosłupa?", CorrectAnswer = 48},
                new Hint{QuestionID = qlist.ElementAt(5).ID, Content = "Zbuduj trójkąt zawierający przekątną tego graniastosłupa, przekątną podstawy oraz wysokość graniastosłupa. Korzystając z podanej wartości kąta pomiędzy przekątną bryły a podstawą, znajdź zależność pomiędzy a i H: H=....*a", CorrectAnswer = 2.5},
                new Hint{QuestionID = qlist.ElementAt(5).ID, Content = "Teraz skorzystajmy z twierdzenia Pitagorasa w tym samym trójkącie oraz z ułożonej przed chwilą zależności. Podaj H", CorrectAnswer = 10},
                new Hint{QuestionID = qlist.ElementAt(5).ID, Content = "W podobny sposób oblicz długość krawędzi podstawy a.", CorrectAnswer = 4},
                new Hint{QuestionID = qlist.ElementAt(5).ID, Content = "Skorzystaj ze wzoru V=Pp*H. Oblicz najpierw pole podstawy:", CorrectAnswer = 16},
                new Hint{QuestionID = qlist.ElementAt(6).ID, Content = "Zastanów się, jakie są względem siebie trójkąty ABC i EBD. Żeby przejść do kolejnej odpowiedzi wpisz 0.", CorrectAnswer = 0},
                new Hint{QuestionID = qlist.ElementAt(6).ID, Content = "Trójkąty te są podobne. Można zatem ułożyć zależności między długościami ich boków, np. |AC|:|ED| = |AB|:|DB|. Oblicz długość boku |AB| = .....+|DB|. Po tym kroku podstaw wartości do ułożonej zależności i oblicz długość odcinka |DB|.", CorrectAnswer = 2},
            };
            foreach (Hint h in hints)
            {
                context.Hint.Add(h);
            }
            context.SaveChanges();
        }
    }
}
