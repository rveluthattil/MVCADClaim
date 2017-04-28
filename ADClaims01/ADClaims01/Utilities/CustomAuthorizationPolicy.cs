using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace ADClaims01.Utilities
{
    public class CustomAuthorizationPolicy : IAuthorizationPolicy
    {
        private Guid id;
        private ClaimSet issuer;
        public static string IssuerName = "http://schemas.ADClaims01.com/2017/04/identity/claims/IssuerName";

        public static class Resources
        {
            public const string Patients =
                "http://schemas.ADClaims01.com/2017/04/identity/resources/patients";
        }

        public static class ClaimTypes
        {
            public const string Create =
                "http://schemas.ADClaims01.com/2017/04/identity/claims/create";
            public const string Read =
                "http://schemas.ADClaims01.com/2017/04/identity/claims/read";
            public const string Update =
                "http://schemas.ADClaims01.com/2017/04/identity/claims/update";
            public const string Delete =
                "http://schemas.ADClaims01.com/2017/04/identity/claims/delete";
        }

        public CustomAuthorizationPolicy()
        {
            id = Guid.NewGuid();
            //Claim claim = Claim.CreateNameClaim("rveluth");
            Claim claim = Claim.CreateNameClaim(CustomAuthorizationPolicy.IssuerName);
            Claim[] claims = new Claim[1];
            claims[0] = claim;
            issuer = new DefaultClaimSet(claims);
        }

        public string Id
        {
            get { return Id.ToString(); }
        }

        public ClaimSet Issuer
        {
            get { return issuer; }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            HttpContext context = HttpContext.Current;
            ClaimSet claimset = GetClaimSetByIdentity(context.User.Identity);

            if (claimset != null)
                evaluationContext.AddClaimSet(this, claimset);
            return true;
        }

        protected virtual ClaimSet GetClaimSetByIdentity(IIdentity identity)
        {
            List<Claim> claims = new List<Claim>();
            //if (!identity.IsAuthenticated)
            //{
            //    claims.Add(new Claim(ClaimTypes.Name, "rveluth", Rights.PossessProperty));
            //}
            //else
            //{
            //    claims.Add(new Claim(ClaimTypes.Name, "rveluth", Rights.PossessProperty));
            //}

            if (!identity.IsAuthenticated)
            {
                claims.Add(new Claim(CustomAuthorizationPolicy.ClaimTypes.Read,
                    CustomAuthorizationPolicy.Resources.Patients, Rights.PossessProperty));
            }
            else
            {
                claims.Add(new Claim(CustomAuthorizationPolicy.ClaimTypes.Create,
                    CustomAuthorizationPolicy.Resources.Patients, Rights.PossessProperty));
                claims.Add(new Claim(CustomAuthorizationPolicy.ClaimTypes.Read,
                    CustomAuthorizationPolicy.Resources.Patients, Rights.PossessProperty));
                claims.Add(new Claim(CustomAuthorizationPolicy.ClaimTypes.Update,
                    CustomAuthorizationPolicy.Resources.Patients, Rights.PossessProperty));
                claims.Add(new Claim(CustomAuthorizationPolicy.ClaimTypes.Delete,
                    CustomAuthorizationPolicy.Resources.Patients, Rights.PossessProperty));

            }

            return new DefaultClaimSet(this.issuer, claims);
        }
    }
}