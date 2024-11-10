using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Invoice
    {
        [Key]
       public int id { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        public float TotalAmount { get; set; }
        
        public float Discount { get; set; } = 0;
       
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }
        
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}