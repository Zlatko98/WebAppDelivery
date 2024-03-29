﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebAppDelivery.Database;
using WebAppDelivery.Models;
using WebAppDelivery.Models.Classes;

namespace WebAppDelivery.Controllers
{
    public class UsersController : ApiController
    {
        //[Authorize(Roles ="Admin,User,Deliverer")]
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

        [Authorize(Roles = "User")]
        [Route("api/users/getme")]
        public EditUserBindingModel GetMe()
        {
            string username = RequestContext.Principal.Identity.Name;

            User user = null;

            try
            {
                using (WebDBContext entities = new WebDBContext())
                {
                    user = entities.Users.FirstOrDefault(d => d.UserName == username);
                    string state = user.UserType.ToString();
                    EditUserBindingModel editUserBindingModel = new EditUserBindingModel
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Surname = user.Surname,
                        BirthDate = user.BirthDate,
                        Address = user.Address
                    };

                    return editUserBindingModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
