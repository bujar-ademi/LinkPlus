using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LinkPlus.dal;
using LinkPlus.dal.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace LinkPlus.dal.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private static UnitOfWork unitOfWork;

        [TestFixtureSetUp]
        public void Init()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
            unitOfWork = new UnitOfWork(new SqlDbContext());
        }

        [Test]
        public void CreateCustomer_Test()
        {
            Customer customer = new Customer()
            {
                FirstName = "Bujar",
                LastName = "Ademi",
                Phone = "+389 71 221 251",
                Email = "bujar@bujarademi.com"
            };
            unitOfWork.Repository<Customer>().Add(customer);
            unitOfWork.SaveChanges();
            Assert.Pass();
        }
        [Test]
        public void CreateOrderWithCustomer_Test()
        {
            Customer customer = unitOfWork.Repository<Customer>().Get(c => c.CustomerId == 1);
            Order order = new Order()
            {
                OrderNo = "1/2018",
                OrderDate = DateTime.Today,
                MethodPayment = MethodPayment.CreditCard
            };
            order.Customer = customer;
            unitOfWork.Repository<Order>().Add(order);
            unitOfWork.SaveChanges();
            Assert.Pass();
        }
        [Test]
        public void CreateCompletOrder_Test()
        {
            Order order = new Order()
            {
                OrderNo = "2/2018",
                OrderDate = DateTime.Today.AddDays(10),
                MethodPayment = MethodPayment.BankTransfer
            };
            order.Customer = new Customer()
            {
                FirstName = "Krenar",
                LastName = "Muhidini",
                Phone = "+389 78 203 320",
                Email = "k.muhidini@linkplus-it.com"
            };
            List<OrderItem> items = new List<OrderItem>();
            items.Add(new OrderItem() { Name = "Web application", Price = 2500, Qty = 1 });
            items.Add(new OrderItem() { Name = "E-Commerce", Price = 1200, Qty = 1 });
            order.OrderItems = items;

            unitOfWork.Repository<Order>().Add(order);
            unitOfWork.SaveChanges();

            Assert.Pass();
        }
    }
}
