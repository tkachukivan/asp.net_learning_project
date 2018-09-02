using ContactManagerBackend.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ContactManagerBackend.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        AuthRepository authRep = new AuthRepository();

        [ActionName("me")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            var userId = GetCurrentUserId();

            IdentityUser user = await authRep.FindUserById(userId);
            
            return Ok(new { user.UserName });
        }

        private string GetCurrentUserId()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return default(string);
            }

            var id = identity.Claims
                             .FirstOrDefault(çlaim => çlaim.Type == "id")?.Value;

            return id;
        }
    }
}
