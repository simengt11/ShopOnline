namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Please enter text!")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Please enter Link!")]
        [StringLength(50)]
        public string Link { get; set; }
        [Required(ErrorMessage = "Please enter DisplayOrder!")]
        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public bool Status { get; set; }

        public int? TypeID { get; set; }
    }
}
