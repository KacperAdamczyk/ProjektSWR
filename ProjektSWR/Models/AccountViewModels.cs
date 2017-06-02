using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektSWR.Models
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Pamiętaj w tej przeglądarce")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "e-mail: (@prz.edu.pl)")]
        [RegularExpression(@"^\w+(a-z0-9)*@prz.edu.pl$", ErrorMessage = "Email musi zawierać domenę 'prz.edu.pl'")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie sa takie same.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Imię nie może mieć więcej niż 16 znaków")]
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string FisrtName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Nazwisko nie może mieć więcej niż 30 znaków")]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Katedra")]
        [RegularExpression(@"^(?!.*--Wybierz katedrę--).*$", ErrorMessage = "Nie wybrano katedry.")] 
        public string CathedralName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "THasła nie sa takie same.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
