using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Policy;
using System.Web.Mvc;
using System.IdentityModel.Claims;
using System.Threading;

namespace ADClaims01.Utilities
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            IUser customuser = filterContext.HttpContext.Session["CustomUser"] as IUser;

            if (customuser == null || customuser.Name != filterContext.HttpContext.User.Identity.Name)
            {
                customuser = new User(filterContext.HttpContext.User.Identity.Name,
                                        filterContext.HttpContext.User.Identity.IsAuthenticated);
                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
                policies.Add(new CustomAuthorizationPolicy());
                System.IdentityModel.Policy.AuthorizationContext authContext =
                    System.IdentityModel.Policy.AuthorizationContext.CreateDefaultAuthorizationContext(policies);

                foreach (ClaimSet claimset in authContext.ClaimSets)
                {
                    //Claim issuerclaim = Claim.CreateNameClaim("rveluth");
                    Claim issuerclaim = Claim.CreateNameClaim(CustomAuthorizationPolicy.IssuerName);
                    if (claimset.Issuer.ContainsClaim(issuerclaim))
                        customuser.Issuer = claimset;
                }
                filterContext.HttpContext.Session["CustomUser"] = customuser;    
            }
            filterContext.HttpContext.User = Thread.CurrentPrincipal = customuser;
        }
    }
}