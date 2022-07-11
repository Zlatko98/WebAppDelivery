using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebAppDelivery.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        [Route("api/users/getpage")]
        public IHttpActionResult GetPage()
        {
            return Ok(new { Value = "Main.html"});
        }
    }
}
