using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.Resources;

namespace Project.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        ////[Required(ErrorMessage = "يرجى إدخال اسم المستخدم")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "UsernameRequiredMessage")]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType =typeof(Resource),ErrorMessageResourceName = "EmailRequiredMessage")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidEmailMessage")]
        public string email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequiredMessage")]
        
        public string PasswordHash { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ActiveSession { get; set; }
    }
}