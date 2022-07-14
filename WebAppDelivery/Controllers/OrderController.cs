using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDelivery.Database;
using WebAppDelivery.Models;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Controllers
{
    public class OrderController : ApiController
    {
        [Authorize]
        [Route("api/order/createorder")]
        [HttpPost]
        public IHttpActionResult CreateOrder([FromBody] OrderProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            string username = RequestContext.Principal.Identity.Name;


            Order order = new Order
            {
                Address = model.Address,
                Comment = model.Comment,
                Total = model.Total,
                Amounts = model.Amounts
            };

            Product product = null;
            User user = null;
            List<Product> products = new List<Product>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    foreach (string name in model.Names)
                    {

                        product = entities.Products.FirstOrDefault(p => p.Name == name);
                        products.Add(product);
                        entities.SaveChanges();
                    }

                    user = entities.Users.FirstOrDefault(p => p.UserName == username);
                    
                    order.Products = products;
                    entities.Orders.Add(order);
                    user.Orders.Add(order);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }

            return Ok();
        }

        [Authorize(Roles ="Deliverer")]
        [Route("api/order/getpendingorders")]
        public List<Order> GetOrders()
        {
            string username = RequestContext.Principal.Identity.Name;
            Deliverer deliverer = null;
            List<Order> orders = new List<Order>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    deliverer = entities.Deliverers.FirstOrDefault(p => p.UserName == username);

                    if (deliverer.UserType == UserType.DELIVERER)
                    {
                        orders = entities.Orders.ToList();
                    }
                }
            }catch(Exception ex)
            {

            }

            return orders;
        }

        [AllowAnonymous]
        [Route("api/order/gettime")]
        [HttpGet]
        public IHttpActionResult GetTime()
        {
            Random rand = new Random();
            int time = rand.Next(15, 60);

            return Ok(new {value = time });
        }

        [Authorize(Roles = "Deliverer")]
        [Route("api/order/takeorder")]
        [HttpPost]
        public IHttpActionResult TakeOrder([FromBody]int id)
        {
            string username = RequestContext.Principal.Identity.Name;

            Deliverer deliverer = null;
            Order order = null;

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {

                    deliverer = entities.Deliverers.FirstOrDefault(p => p.UserName == username);
                    order = entities.Orders.FirstOrDefault(o => o.Id == id);
                    order.OrderState = OrderState.INPROGRESS;
                    deliverer.Orders.Add(order);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }

            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("api/order/getuserorders")]
        public List<Order> GetUserOrders()
        {
            string username = RequestContext.Principal.Identity.Name;
            User user = null;
            List<Order> orders = new List<Order>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    user = entities.Users.FirstOrDefault(p => p.UserName == username);

                    foreach(Order o in user.Orders)
                    {
                        orders.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return orders;
        }
    }
}
