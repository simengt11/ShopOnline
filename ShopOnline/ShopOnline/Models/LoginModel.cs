using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please enter your user name!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string  Password { get; set; }
    }
}