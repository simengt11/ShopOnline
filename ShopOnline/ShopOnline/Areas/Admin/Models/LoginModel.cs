using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời nhập Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mời nhập Password")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}