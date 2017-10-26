using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Math_Project.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Musisz coś tu wpisać")]
        [EmailAddress(ErrorMessage = "Coś tu chyba źle wpisałeś kolego")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej {2} znaków (a maksymalnie {1})", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło wpisz jeszcze raz")]
        [Compare("Password", ErrorMessage = "Hasła się nie zgadzają kolego")]
        public string ConfirmPassword { get; set; }
    }
}
