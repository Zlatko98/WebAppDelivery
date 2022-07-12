using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using WebAppDelivery.Database;

[assembly: OwinStartup(typeof(WebAppDelivery.Startup))]

namespace WebAppDelivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            using (var context = new WebDBContext())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}
