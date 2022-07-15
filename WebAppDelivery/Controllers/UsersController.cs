using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebAppDelivery.Database;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        [Route("api/users/getpage")]
        public IHttpActionResult GetPage()
        {
            string username = RequestContext.Principal.Identity.Name;

            User user = null;
            Deliverer deliverer = null;

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    if ((user = entities.Users.FirstOrDefault(d => d.UserName == username)) == null)
                    {
                        deliverer = entities.Deliverers.FirstOrDefault(d => d.UserName == username);
                        return Ok(new { Value = "MainDeliverer.html" });
                    }
                    else
                    {
                        if (user.UserType == UserType.ADMINISTRATOR)
                        {
                            return Ok(new { Value = "MainAdmin.html" });
                        }
                        else
                        {
                            return Ok(new { Value = "MainUser.html" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }
        }

        [Authorize(Roles ="Deliverer")]
        [Route("api/users/getstate")]
        public IHttpActionResult GetState()
        {
            string username = RequestContext.Principal.Identity.Name;

            Deliverer deliverer = null;

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    deliverer = entities.Deliverers.FirstOrDefault(d => d.UserName == username);
                    string state = deliverer.UserType.ToString();
                    return Ok(new { Value = state });

                }
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }
        }




    }
}
