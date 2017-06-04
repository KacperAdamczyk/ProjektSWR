using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektSWR.Models
{
    public class ManageViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public bool BrowserRemembered { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage ="Pole {0} jest wymagane.")]
        [DataType(DataType.Text)]
        [StringLength(16, ErrorMessage = "Pole {0} może mieć maksymalnie {1} znaków.")]
        [Display(Name = "imię")]
        [RegularExpression(@"^[A-Z]{1}[a-z]+$", ErrorMessage = "Pole {0} musi składać się ze znaków z przedziału [a-z]. Pierwsza litera musi być duża!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Pole {0} może mieć maksymalnie {1} znaków.")]
        [Display(Name = "nazwisko")]
        [RegularExpression(@"^(([A-Z]{1}[a-z]+)|([A-Z]{1}[a-z]+[\-]{1}[A-Z]{1}[a-z]+))$", ErrorMessage = "Pole {0} musi składać się ze znaków z przedziału [a-z]. Pierwsza litera musi być duża!")]
        public string LastName { get; set; }

        [Required]
        public string AcademicDegree { get; set; }

        [DataType(DataType.Url)]
        public string Photo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "opis")]
        [StringLength(500, ErrorMessage = "Pole {0} może mieć maksymalnie {1} znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "e-mail")]
        [RegularExpression(@"^\w+(a-z0-9)*@prz.edu.pl$", ErrorMessage = "Email musi zawierać domenę 'prz.edu.pl'")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "numer telefonu")]
        [RegularExpression(@"^(([0-9]{9})|([0-9]{3}[\s\-]{1}[0-9]{3}[\s\-]{1}[0-9]{3})|([\(]{1}[0-9]{3}[\)]{1}[\s\-]{1}[0-9]{3}[\s\-]{1}[0-9]{4})|([0-9]{3}[\s\-]{1}[0-9]{3}[\s\-]{1}[0-9]{3}[\s\-]{1}[0-9]{3}))$", ErrorMessage = "Numer telefonu został błędnie podany")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Katedra: ")]
        public string CathedralName { get; set; }


        public IList<Cathedral> Cathedrals;

        public class Degree
        {
            public string Value { get; set; }
        };

        public IEnumerable<Degree> AcademicDegreeOptions = new List<Degree>
        {
            new Degree { Value = "brak" },
            new Degree { Value = "inż." },
            new Degree { Value = "mgr" },
            new Degree { Value = "mgr inż." },
            new Degree { Value = "dr" },
            new Degree { Value = "dr hab." },
            new Degree { Value = "prof." },
        };
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Stare hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Błędne powtórzenie hasła.")]
        public string ConfirmPassword { get; set; }
    }

}