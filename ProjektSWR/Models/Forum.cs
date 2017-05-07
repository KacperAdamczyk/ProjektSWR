using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProjektSWR.Models
{
    public class Thread
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string MainMessage { get; set; }
        [Required] public Category CategoryID { get; set; }
        [Required] public ApplicationUser UserID { get; set; }
        [Required] public Admin AdminID { get; set; }

    }

    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public Forum ForumID { get; set; }
    }

    public class Forum
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
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