﻿using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Models.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetcoreEcommercedemo.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<SpecialTag> SpecialTags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<MyMessage> MyMessages { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
    }
}
