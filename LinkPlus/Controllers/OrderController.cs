using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinkPlus.dal.Models;
using LinkPlus.BusinessLayer;
namespace LinkPlus.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly ICustomerLayer customerLayer;
        private readonly IOrderLayer orderLayer;

        public OrderController()
        {
            customerLayer = new CustomerLayer();
            orderLayer = new OrderLayer();
        }
        /// <summary>
        /// Used for DI
        /// </summary>
        /// <param name="work"></param>
        public OrderController(ICustomerLayer customer, IOrderLayer order)
        {
            customerLayer = customer;
            orderLayer = order;
        }

        [Route("{OrderId}")]
        public IHttpActionResult GetOrderById(int OrderId)
        {
            Order order = orderLayer.GetOrder(OrderId);
            return Ok(order);
        }
        [Route("{FromDate}/{ToDate}")]
        public IHttpActionResult GetByDateRange(DateTime FromDate, DateTime ToDate)
        {
            IList<Order> list = orderLayer.GetOrders(FromDate, ToDate);
            return Ok(list);
        }

        [Route("List")]
        public IHttpActionResult GetList()
        {
            IList<Order> list = orderLayer.GetOrders();
            return Ok(list);
        }

        public IHttpActionResult Post(Order model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            model = orderLayer.AddOrder(model);
            return Ok(model);
        }

    }
}