using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailValidationController : ControllerBase
    {
        public EmailValidationController()
        {

        }

        public ActionResult Post(string token)
        {

        }
    }
}