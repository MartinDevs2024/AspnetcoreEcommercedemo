using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Special Tags")]
        public string Name { get; set; }
    }
}
