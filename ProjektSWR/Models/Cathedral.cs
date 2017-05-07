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
        [Required] public string Department { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Page { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}