namespace LinkPlus.dal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            //Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }

        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(128)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}
