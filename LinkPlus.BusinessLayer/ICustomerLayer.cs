using System.Collections.Generic;
using LinkPlus.dal.Models;
namespace LinkPlus.BusinessLayer
{
    public interface ICustomerLayer
    {
        Customer GetCustomer(int CustomerId);
        Customer AddCustomer(Customer customer);
        IList<Customer> GetCustomers();
        bool RemoveCustomer(int CustomerId);
    }
}
