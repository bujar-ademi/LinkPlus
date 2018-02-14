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
        private readonly IRepository<Customer> Repository;

        //In case we want to use DI
        public CustomerLayer(UnitOfWork work)
        {
            unitOfWork = work;
            Repository = unitOfWork.Repository<Customer>();
        }
        public CustomerLayer()
        {
            Repository = new UnitOfWork(new SqlDbContext()).Repository<Customer>();
        }
        public Customer AddCustomer(Customer customer)
        {
            //here we make checks if everything is ok with the Customer model before saving to db
            Repository.Add(customer);
            unitOfWork.SaveChanges();
            return customer;
        }

        public Customer GetCustomer(int CustomerId)
        {
            var customer = Repository.Get(c => c.CustomerId == CustomerId);
            if(customer == null)
            {
                //we can throw exception or something in business layer
            }
            return customer;
        }

        public IList<Customer> GetCustomers()
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
