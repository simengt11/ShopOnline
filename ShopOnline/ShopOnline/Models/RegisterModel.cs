using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Please enter your username!")]
        public string Username { get; set; }

        [MinLength(6,ErrorMessage ="At least 6 symbol")]
        [Required(ErrorMessage ="Please enter you password!")]
        public string Password { get; set; }

        [MinLength(6, ErrorMessage = "At least 6 symbol")]
        [Required(ErrorMessage = "Please enter you password!")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }
}