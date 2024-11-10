using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.ViewModel
{
    public class Create_Invoice
    {
        
        public DateTime InvoiceDate { get; set; }
       
        public float TotalAmount { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public float Discount { get; set; } = 0;

        [Required]
        public List<InvoiceItem> Items { get; set; }
    }
}