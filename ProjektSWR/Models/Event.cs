using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace ProjektSWR.Models
{
    public class GlobalEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Coordinator { get; set; }
        [Required] public virtual ApplicationUser UserID { get; set; }
        [Required] public virtual Admin AdminID { get; set; }

    }

    public class PrivateEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public virtual ApplicationUser UserID { get; set; }
        [Required] public virtual Admin AdminID { get; set; }

    }

    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Title { get; set; }
        [Required] public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public virtual GlobalEvent GlobalEventID { get; set; }
        public virtual PrivateEvent PrivateEventID { get; set; }
    }
}