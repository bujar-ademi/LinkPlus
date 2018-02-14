namespace LinkPlus.dal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderItem")]
    public partial class OrderItem
    {
        [Key]
        public int ItemId { get; set; }

        public int OrderId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public double Price { get; set; }

        public double Qty { get; set; }

        public virtual Order Order { get; set; }
    }
}
