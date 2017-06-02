using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace ProjektSWR.Models
{
    public class Thread
    {
        public Thread()
        {
            Replies = new List<Reply>();
            Notifications = new List<Notification>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string MainMessage { get; set; }
        [Required] public string Email { get; set; }
        [Required] public Category CategoryID { get; set; }
        [Required] public ApplicationUser UserID { get; set; }
        

        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }

    public class Category
    {
        public Category()
        {
            Threads = new List<Thread>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public Forum ForumID { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
    }

    public class Forum
    {
        public Forum()
        {
            Categories = new List<Category>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }

    public class Reply
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Answer { get; set; }
        [Required] public string Email { get; set; }
        [Required] public Thread ThreadID { get; set; }
    }
}