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
        [Authorize(Roles ="User")]
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
                OrderState = OrderState.PENDING,
            };

            Product product = null;
            User user = new User();
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
        public List<Order> GetPendingOrders()
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
                        foreach (Order o in deliverer.Orders)
                        {
                            if(o.OrderState == OrderState.INPROGRESS)
                            {
                                return orders;
                            }
                        }
                        
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
        public IHttpActionResult TakeOrder(SetDelivererBindingModel m)
        {
            string username = RequestContext.Principal.Identity.Name;

            Deliverer deliverer = null;
            Order order = new Order();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {

                    deliverer = entities.Deliverers.FirstOrDefault(p => p.UserName == username);
                    order = entities.Orders.FirstOrDefault(o => o.Id == m.Id);
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

        [Authorize(Roles = "Deliverer")]
        [Route("api/order/getdeliverersorders")]
        public List<Order> GetDeliverersOrders()
        {
            string username = RequestContext.Principal.Identity.Name;
            Deliverer user = null;
            List<Order> orders = new List<Order>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    user = entities.Deliverers.FirstOrDefault(p => p.UserName == username);

                    foreach (Order o in user.Orders)
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
        [Authorize(Roles = "Admin")]
        [Route("api/order/getadminorders")]
        public List<Order> GetAdminOrders()
        {
            string username = RequestContext.Principal.Identity.Name;
            List<Order> orders = new List<Order>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    orders = entities.Orders.ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return orders;
        }


    }
}
