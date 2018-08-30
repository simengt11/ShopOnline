namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Please enter your name!")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your phone!")]
        [StringLength(50)]

        public string Phone { get; set; }
        [StringLength(50)]

        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your content!")]
        [StringLength(250)]
        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}
