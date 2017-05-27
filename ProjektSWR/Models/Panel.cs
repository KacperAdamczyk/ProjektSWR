using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ProjektSWR.Models
{
    public class ManageUsersModel
    {
        [EmailAddress]
        [Display(Name = "e-mail: (@prz.edu.pl)")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Katedra")]
        public string CathedralName { get; set; }

        public string Id { get; set; }

        [Display(Name = "Data blokady")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LockDate { get; set; }
    }
    public class UserProfileModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Stopień naukowy")]
        public string AcademicDegree { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string DateOfBirth { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Strona domowa")]
        public string Homepage { get; set; }

        [EmailAddress]
        [Display(Name = "e-mail: (@prz.edu.pl)")]
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Katedra")]
        public string CathedralName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}