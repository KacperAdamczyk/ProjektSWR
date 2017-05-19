using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}