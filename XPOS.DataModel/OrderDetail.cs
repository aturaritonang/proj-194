namespace XPOS.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }
    }
}
