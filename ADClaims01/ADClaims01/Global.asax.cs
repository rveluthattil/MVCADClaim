using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ADClaims01.Utilities; 

namespace ADClaims01
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
