using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_wiadomosci { get; set; }
        //[ForeignKey("Nadawca")]
        public int ID_nadawcy { get; set; }
        public int ID_odbiorcy { get; set; }
        [Required]
        public string Temat { get; set; }
        public string Tresc { get; set; }
        [Required]
        public DateTime Data_nadania { get; set; }
        public DateTime Data_odbioru { get; set; }
        public bool Archiwizacja_odbiorca { get; set; }
        public bool Archiwizacja_nadawca { get; set; }
        public int ID_odpowiedzi { get; set; }
    }

    public class MessageDBContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}