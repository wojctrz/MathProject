using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MathProject.Models
{
    public enum Categories
    {
        Trygonometria,
        Stereometria,
        Planimetria,
        [DisplayName("Funkcja kwaderatow")]
        FunkcjaKwadratowa
    }
    public class Question
    {
        public int ID { get; set; }
        [DisplayName("Kategoria")]
        public Categories Category { get; set; }
        [DisplayName("Treść")]
        public string Content { get; set; }
        [DisplayName("Poprawna odpowiedź")]
        public double CorrectAnswer { get; set; }
        [DisplayName("Podpowiedzi")]
        public ICollection<Hint> Hints { get; set; }
    }
}
