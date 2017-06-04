using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;


namespace ProjektSWR.Models
{
    public class GlobalEvent
    {
        public GlobalEvent()
        {
            Events = new List<Event>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Coordinator { get; set; }
        [Required] public virtual ApplicationUser UserID { get; set; }
        [Required] public virtual Admin AdminID { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }

    public class PrivateEvent
    {
        public PrivateEvent()
        {
            Events = new List<Event>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public virtual ApplicationUser UserID { get; set; }
        [Required] public virtual Admin AdminID { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }

    public class Event
    {
        public Event()
        {
            Notifications = new List<Notification>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name ="Tytuł")]
        [Required] public string Title { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        [Required] public DateTime StartDate { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Lokacja")]
        public string Location { get; set; }
        [Display(Name = "Szczegóły")]
        public string Details { get; set; }
        public virtual GlobalEvent GlobalEventID { get; set; }
        public virtual PrivateEvent PrivateEventID { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}