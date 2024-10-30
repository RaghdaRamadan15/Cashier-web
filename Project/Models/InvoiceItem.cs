using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class InvoiceItem
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; } 
        public Invoice Invoice { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; } 
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public float UnitPrice { get; set; }
        public float Discount { get; set; } = 0; 
        public float TotalPrice { get; set; }
    }
}