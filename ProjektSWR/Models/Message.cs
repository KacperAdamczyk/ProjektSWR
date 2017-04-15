using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WiadomoscID { get; set; }
        public virtual ApplicationUser NadawcaID { get; set; }
        public string OdbiorcaID { get; set; }
        [Required]
        public string Temat { get; set; }
        public string Tresc { get; set; }
        [Required]
        public DateTime Data_nadania { get; set; }
        public DateTime? Data_odbioru { get; set; }
        [Required, DefaultValue(false)]
        public bool Archiwizacja_odbiorca { get; set; }
        [Required, DefaultValue(false)]
        public bool Archiwizacja_nadawca { get; set; }
        [DefaultValue(null)]
        public Message OdpowiedzID { get; set; }
    }
}