using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Project_OnlineBookStore.Models;

namespace Project_OnlineBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Orders> Orders { get; set; }

       
        public DbSet<Report> Report { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}
