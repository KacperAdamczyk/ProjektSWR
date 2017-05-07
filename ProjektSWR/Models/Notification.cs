using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Contents { get; set; }
        [Required] public string Status { get; set; }
        [Required] public ApplicationUser UserID { get; set; }
        public Event EventID { get; set; }
        public Message MessageID { get; set; }
        public Thread ThreadID { get; set; }
    }
}