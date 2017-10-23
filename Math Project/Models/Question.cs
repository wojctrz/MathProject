using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Categories Category { get; set; }
        public string Content { get; set; }
        public decimal CorrectAnswer { get; set; }
        public ICollection<Hint> Hints { get; set; }
    }
}
