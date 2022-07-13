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
        [AllowAnonymous]
        [Route("api/order/placeorder")]
        [HttpPost]
        public IHttpActionResult PlaceOrder([FromBody]OrderProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order
            {
                Address = model.Address,
                Comment = model.Comment,
                Total = model.Total,
                Amounts = model.Amounts
            };

            Product product = null;
            List<Product> products = new List<Product>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    foreach (string name in model.Names) {

                        product = entities.Products.FirstOrDefault(p => p.Name == name);
                        products.Add(product);
                        entities.SaveChanges();
                    }

                    order.Products = products;

                    entities.Orders.Add(order);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }


            return Ok();
        }
    }
}
