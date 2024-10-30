using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class SalesContext:DbContext
    {

        public SalesContext() : base("name=MyDbConnection")
        {

        }
        public DbSet<User> Users {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }



    }
}