using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.ViewModels
{
    public class ContactsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CurrentImage { get; set; }

        [Display(Name = "Please choose an image")]
        public IFormFile Photo { get; set; }
    }
}
