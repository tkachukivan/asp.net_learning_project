using ContactManagerBackend.Models;
using ContactManagerBackend.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ContactManagerBackend.Repository
{
    public class RegistrationResult
    {
        public IdentityResult result { get; set; }
        public string access_token { get; set; }
    }

    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        static public OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
        {
            AllowInsecureHttp = true,
            TokenEndpointPath = new PathString("/api/auth/login"),
            AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            Provider = new SimpleAuthorizationServerProvider()
        };

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<RegistrationResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            Microsoft.Owin.Security.AuthenticationTicket at = new Microsoft.Owin.Security.AuthenticationTicket(new ClaimsIdentity("Bearer", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"),
                new Microsoft.Owin.Security.AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1) // whenever you want your new token's expiration to happen
                });

            at.Identity.AddClaim(new Claim("name", user.UserName));
            at.Identity.AddClaim(new Claim("userRole", "user"));
            at.Identity.AddClaim(new Claim("id", user.Id));
            // and so on
            string token = OAuthServerOptions.AccessTokenFormat.Protect(at);

            return new RegistrationResult() { result = result, access_token = token };
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityUser> FindUserById(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}