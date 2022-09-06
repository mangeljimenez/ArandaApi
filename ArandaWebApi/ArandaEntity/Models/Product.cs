namespace ArandaEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [Key]
        public int idProduct { get; set; }

        [Required]
        [StringLength(120)]
        public string productName { get; set; }

        [StringLength(512)]
        public string description { get; set; }

        public int idProductCategory { get; set; }

        [Column(TypeName = "image")]
        public byte[] productImage { get; set; }

        public bool isActive { get; set; }

        public virtual Category Category { get; set; }
    }
}
