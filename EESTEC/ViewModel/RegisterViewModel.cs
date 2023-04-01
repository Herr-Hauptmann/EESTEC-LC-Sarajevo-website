using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email je obavezan za registraciju")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna za registraciju")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Lozinka mora sadržavati najmanje 6 znakova, veliko slovo, cifru i specijalni karakter!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Obavezno je potvrdidi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke moraju biti iste")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Name")]    
        [Required(ErrorMessage = "Ime je obavezno za registraciju!")]
        public string Name { get; set; }

        [Display(Name = "Keyword")]    
        [Required(ErrorMessage = "Keyword je obavezan za registraciju")]
        public string Keyword { get; set; }
    }
}
