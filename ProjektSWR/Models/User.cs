using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string AcademicDegree { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Login { get; set; }
        //public image... Photo {get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
    }

    public class NormalUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }

    public class Administrator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}