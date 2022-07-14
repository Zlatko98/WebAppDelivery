using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDelivery.Database;
using WebAppDelivery.Models;
using WebAppDelivery.Models.Classes;
using System.Net.Mail;

namespace WebAppDelivery.Controllers
{
    public class AdminController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("api/admin/createproduct")]
        [HttpPost]
        public IHttpActionResult CreateProduct([FromBody]ProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Ingredients = model.Ingredients
            };

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    entities.Products.Add(product);
                    entities.SaveChanges();

                }
            }
            //catch (Exception ex)
            //{
            //    return (BadRequest(ex.Message));
            //}
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route("api/admin/getdeliverers")]
        public List<Deliverer> GetDeliverers()
        {
             List<Deliverer> deliverers = new List<Deliverer>();

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    deliverers = entities.Deliverers.ToList();
                }
            }
            catch (Exception ex)
            {
                
            }

            return deliverers;
        }

        [Authorize(Roles = "Admin")]
        [Route("api/admin/setdeliverer")]
        [HttpPost]
        public IHttpActionResult SetDeliverer([FromBody] SetDelivererBindingModel model)
        {
            Deliverer deliverer = null;
            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    deliverer = entities.Deliverers.FirstOrDefault(d => d.Id == model.Id);
                    deliverer.UserType = UserType.DELIVERER;
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }

            #region Mail
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("zlatkolukic998@gmail.com");
            mm.To.Add(deliverer.Email);
            mm.Subject = "Account state changed";
            mm.Body = "Your account is set to DELIVERER.";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("zlatkolukic998@gmail.com", "cavoledeni4323");
            smtpClient.Send(mm);
            #endregion

            return Ok();
        }

        [Route("api/admin/blockdeliverer")]
        [HttpPost]
        public IHttpActionResult BlockDeliverer([FromBody] SetDelivererBindingModel model)
        {
            Deliverer deliverer = null;
            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    deliverer = entities.Deliverers.FirstOrDefault(d => d.Id == model.Id);
                    deliverer.UserType = UserType.REJECTED;
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }
            #region Mail
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("zlatkolukic998@gmail.com");
            mm.To.Add(deliverer.Email);
            mm.Subject = "Account state changed";
            mm.Body = "Your account is REJECTED.";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("zlatkolukic998@gmail.com", "cavoledeni4323");
            smtpClient.Send(mm);
            #endregion

            return Ok();
        }

    }
}
