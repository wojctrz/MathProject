using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MathProject.Models
{
    public enum Categories
    {
        Trygonometria,
        Stereometria,
        Planimetria,
        [Display(Name = "Funkcja Kwadratowa")]
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
