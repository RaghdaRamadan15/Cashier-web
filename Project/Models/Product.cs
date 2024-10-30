using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string ProductName { get; set; }
        
        public string image { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
       
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
    }
}