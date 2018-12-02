using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace TudoBuffet.Website.Controllers
{
    public class AuthenticatedControllerBase : ControllerBase
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

        protected ActionResult ServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
