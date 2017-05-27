﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Message
    {
        public Message()
        {
            Recipients = new List<Recipient>();
            Notifications = new List<Notification>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public virtual ApplicationUser SenderID { get; set; }
        [Required] public string Subject { get; set; }
        public string Content { get; set; }
        [Required] public DateTime SendDate { get; set; }
        [Required, DefaultValue(false)] public bool Archived { get; set; }
        [DefaultValue(null)] public virtual Message ResponseID { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }

    public class Recipient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public virtual Message MessageID { get; set; }
        [Required] public virtual ApplicationUser UserID { get; set; }
        [Required, DefaultValue(false)] public bool Archived { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}