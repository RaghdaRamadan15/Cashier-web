using Project.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.ViewModel
{
    public class login
    {
        [Required(ErrorMessage ="ادخل اميلك")]
        public string email { get; set; }
        [Required(ErrorMessage = "ادخل باسورد")]
             
        
        public string PasswordHash { get; set; }
    }
}