using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MathProject.Models
{
    public class Hint
    {
        public int ID { get; set; }
        [DisplayName("ID pytania")]
        public int QuestionID { get; set; }
        [DisplayName("Treść")]
        public string Content { get; set; }
        [DisplayName("Poprawna odpowiedź")]
        public double CorrectAnswer { get; set; }
    }
}
