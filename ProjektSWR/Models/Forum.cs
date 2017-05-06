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

    }

    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
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
    }
}