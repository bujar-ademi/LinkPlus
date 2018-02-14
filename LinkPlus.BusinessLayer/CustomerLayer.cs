using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkPlus.dal;
using LinkPlus.dal.Models;
using LinkPlus.dal.Repository;
namespace LinkPlus.BusinessLayer
{
    public class CustomerLayer : ICustomerLayer
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IRepository<Order> Repository;

        //In case we want to use DI
        public CustomerLayer(UnitOfWork work)
        {
            unitOfWork = work;
            Repository = unitOfWork.Repository<Order>();
        }
        public CustomerLayer()
        {
            Repository = new UnitOfWork(new SqlDbContext()).Repository<Order>();
        }
        public Order AddCustomer(Order customer)
        {
            //here we make checks if everything is ok with the Customer model before saving to db
            Repository.Add(customer);
            unitOfWork.SaveChanges();
            return customer;
        }

        public Order GetCustomer(int CustomerId)
        {
            var customer = Repository.Get(c => c.CustomerId == CustomerId);
            if(customer == null)
            {
                //we can throw exception or something in business layer
            }
            return customer;
        }

        public IList<Order> GetCustomers()
        {
            return Repository.GetAll().ToList();
        }

        public bool RemoveCustomer(int CustomerId)
        {
            //before removing need to check if exists
            var customer = Repository.Get(c => c.CustomerId == CustomerId);
            if (customer == null)
                return false;
            Repository.Delete(customer);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
