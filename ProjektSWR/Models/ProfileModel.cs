using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace ProjektSWR.Models
{
    public class ProfileModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string AcademicDegree { get; set; }

        public string Photo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia: ")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Opis: ")]
        public string Description { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Katedra: ")]
        public string CathedralName { get; set; }
    }
}