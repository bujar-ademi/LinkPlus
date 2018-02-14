namespace LinkPlus.dal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext()
            : base("name=SqlDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlDbContext, Migrations.Configuration>("SqlDbContext"));
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
