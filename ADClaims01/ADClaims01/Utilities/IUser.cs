using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.IdentityModel.Claims;

namespace ADClaims01.Utilities
{
    public interface IUser : IPrincipal
    {
        bool IsAuthenticated { get; }
        string Name { get; }
        ClaimSet Issuer { get; set; }
    }


    public class User : IUser
    {

        public User(string name, bool isAutheticated)
        {
            this.Identity = new GenericIdentity(name);
        }
        public bool isInRole(string role)
        {
            throw new NotImplementedException();
        }
        public IIdentity Identity
        {
            get;
        }

        public bool IsAuthenticated
        {
            get
            {
                return Identity.IsAuthenticated;
            }
        }

        private ClaimSet issuer;
        public ClaimSet Issuer
        {
            get
            {
                return issuer;
            }

            set
            {
                issuer = value;
            }
        }

        public string Name
        {
            get
            {
                return Identity.Name;
            }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}