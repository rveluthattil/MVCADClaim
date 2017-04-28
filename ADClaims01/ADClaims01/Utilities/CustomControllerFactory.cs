using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ADClaims01.Utilities
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController iController = base.GetControllerInstance(requestContext, controllerType);

            if (typeof(Controller).IsAssignableFrom(controllerType))
            {
                Controller controller = iController as Controller;

                if (null != controller)
                {
                    controller.ActionInvoker = new CustomControllerActionInvoker();
                    return iController;
                }
            }
            return iController;
        }
    }
}