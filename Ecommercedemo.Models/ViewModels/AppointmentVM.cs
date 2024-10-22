﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Models.ViewModels
{
    public class AppointmentVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string OnwerId { get; set; }
        public string CustomerId { get; set; }
        public bool IsOwnerApproved { get; set; }
        public string AdminId { get; set; }

        public string OwnerName { get; set; }

        public string CustomerName { get; set; }

        public string AdminName { get; set; }

        public bool IsForClient { get; set; }
    }
}
