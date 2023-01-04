using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Models
{
    public class TodoList
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
