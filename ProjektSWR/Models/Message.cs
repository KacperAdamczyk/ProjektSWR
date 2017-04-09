using System;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Message
    {
        public int ID_wiadomosci { get; set; }
        public int ID_nadawcy { get; set; }
        public int ID_odbiorcy { get; set; }
        public string Temat { get; set; }
        public string Tresc { get; set; }
        public DateTime Data_nadania { get; set; }
        public DateTime Data_odbioru { get; set; }
        public bool Archiwizacja { get; set; }
        public int ID_odpowiedzi { get; set; }
    }

    public class MessageDBContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}