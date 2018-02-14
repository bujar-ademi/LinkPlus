using System;
using System.Collections.Generic;
using LinkPlus.dal.Models;

namespace LinkPlus.BusinessLayer
{
   public interface IOrderLayer
    {
        Order GetOrder(int OrderId);
        IList<Order> GetOrders();
        IList<Order> GetOrders(DateTime FromDate, DateTime toDate);

        Order AddOrder(Order order);

    }
}
