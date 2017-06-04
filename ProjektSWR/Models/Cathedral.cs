using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace ProjektSWR.Models
{
    public class Cathedral
    {
        public Cathedral()
        {
            Users = new List<ApplicationUser>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name ="Nazwa")]
        [Required] public string Department { get; set; }
        [Display(Name ="Adres")]
        [Required] public string Address { get; set; }
        [Display(Name = "Strona domowa")]
        [Required] public string Page { get; set; }
        [Display(Name = "Numer telefonu")]
        [Required] public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [Required] public string Email { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}