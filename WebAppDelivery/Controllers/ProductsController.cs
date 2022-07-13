using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDelivery.Database;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Controllers
{
    public class ProductsController : ApiController
    {
        [Route("api/Products/GetProducts")]
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    products = entities.Products.ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return products;
        }
    }
}
