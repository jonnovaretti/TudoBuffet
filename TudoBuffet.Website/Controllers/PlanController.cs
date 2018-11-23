using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/planos")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlans plans;

        public PlanController(IPlans plans)
        {
            this.plans = plans;
        }

        public ActionResult<List<Plan>> Get()
        {
            return Ok(plans.GetAll());
        }
    }
}