namespace LinkPlus.dal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum MethodPayment:short
    {
        CreditCard,
        Cash,
        BankTransfer
    }
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public MethodPayment MethodPayment { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
