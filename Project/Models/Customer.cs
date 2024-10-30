using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.Resources;

namespace Project.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string CustomerName { get; set; }
       
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}