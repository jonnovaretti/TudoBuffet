using Microsoft.AspNetCore.Mvc;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/validacao-email")]
    [ApiController]
    public class EmailValidationController : ControllerBase
    {
        IEmailValidator emailValidatorService;

        public EmailValidationController(IEmailValidator emailValidatorService)
        {
            this.emailValidatorService = emailValidatorService;
        }

        [HttpPost]
        public ActionResult Post(dynamic payload)
        {
            emailValidatorService.ActiveEmail(payload.token.ToString());

            return Ok();
        }
    }
}