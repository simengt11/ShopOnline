using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShopOnline.Models
{
    public class CartInforModel
    {
        [DisplayName("Customer Name")]
        [StringLength(250)]
        [Required(ErrorMessage ="Please enter the name whose recieve this order!")]
        public string ShipName { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please enter the customer phone!")]
        [StringLength(50)]
        public string ShipPhone { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter the adress!")]
        [StringLength(250)]
        public string ShipAddress { get; set; }

        [DisplayName("Email")]
        [StringLength(150)]
        public string ShipEmail { get; set; }
    }
}