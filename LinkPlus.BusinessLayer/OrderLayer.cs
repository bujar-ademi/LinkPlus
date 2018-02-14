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
    public class OrderLayer : IOrderLayer
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IRepository<Order> Repository;

        //In case we want to use DI
        public OrderLayer(UnitOfWork work)
        {
            unitOfWork = work;
            Repository = unitOfWork.Repository<Order>();
        }
        public OrderLayer()
        {
            Repository = new UnitOfWork(new SqlDbContext()).Repository<Order>();
        }
        public Order AddOrder(Order order)
        {
            //we can make here a lot of checks before saving to db, for simplicity I am adding directly to db
            Repository.Add(order);
            unitOfWork.SaveChanges();
            return order;
        }

        public Order GetOrder(int OrderId)
        {
            var order = Repository.Get(o => o.OrderId == OrderId);
            if(order == null)
            {
                //throw exception or do something
            }
            return order;
        }

        public IList<Order> GetOrders()
        {
            return Repository.GetAll().ToList();
        }

        public IList<Order> GetOrders(DateTime FromDate, DateTime toDate)
        {
            return Repository.GetAll(o => o.OrderDate >= FromDate && o.OrderDate <= toDate).ToList();
        }
    }
}
