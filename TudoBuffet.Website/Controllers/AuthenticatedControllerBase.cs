using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace TudoBuffet.Website.Controllers
{
    public class AuthenticatedControllerBase : Controller
    {
        public Guid UserId
        {
            get
            {
                Claim claim;
                Guid idParsed;

                claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                idParsed = Guid.Parse(claim.Value);

                return idParsed;
            }
        }

        public string FullName
        {
            get
            {
                Claim claim;

                claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "fullname");

                return claim.Value;
            }
        }

        protected ActionResult ServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
