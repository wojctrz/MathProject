using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MathProject.Models
{
    public enum Categories
    {
        Trygonometria,
        Stereometria,
        Planimetria,
        FunkcjaKwadratowa
    }
    public class Question
    {
        public int ID { get; set; }
        [DisplayName("Kategoria")]
        public Categories Category { get; set; }
        //[StringLength(100, ErrorMessage = "musi mieć co najmniejznaków (a maksymalnie xd)", MinimumLength = 6)]
        [DisplayName("Treść pytania")]
        public string Content { get; set; }  
        [DisplayName("Prawidłowa odpowiedź")]
        public decimal CorrectAnswer { get; set; }
        public ICollection<Hint> Hints { get; set; }
    }
}
