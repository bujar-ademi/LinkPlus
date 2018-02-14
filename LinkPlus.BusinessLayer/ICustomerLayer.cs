using System.Collections.Generic;
using LinkPlus.dal.Models;
namespace LinkPlus.BusinessLayer
{
    public interface ICustomerLayer
    {
        Order GetCustomer(int CustomerId);
        Order AddCustomer(Order customer);
        IList<Order> GetCustomers();
        bool RemoveCustomer(int CustomerId);
    }
}
