﻿using AspnetcoreEcommercedemo.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Body { get; set; } = "";

        public string Description { get; set; } = "";

        public string Tags { get; set; } = "";

        public string Category { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<Photo> Photos { get; set; }
        public List<MainComment> MainComments { get; set; }
    }
}
