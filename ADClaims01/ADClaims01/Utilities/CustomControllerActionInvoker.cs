using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADClaims01.Utilities
{
    public class CustomControllerActionInvoker : ControllerActionInvoker
    {
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IAuthorizationFilter authorizationFilter = new AuthorizationFilter();
            FilterInfo baseFilters = base.GetFilters(controllerContext, actionDescriptor);
            baseFilters.AuthorizationFilters.Add(authorizationFilter);
            return baseFilters;
        }
    }
}