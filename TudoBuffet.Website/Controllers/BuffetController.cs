using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/buffets")]
    [AllowAnonymous]
    [ApiController]
    public class BuffetController : ControllerBase
    {
        [Route("destaques")]
        public ActionResult<List<BuffetTopModel>> GetHotest()
        {
            List<BuffetTopModel> buffets = new List<BuffetTopModel>();
            BuffetTopModel buffet = new BuffetTopModel();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Afro Festa";
            buffet.Category = BuffetCategory.Infantil;
            buffet.Thumbprint = "img/product3_2.jpg";

            buffets.Add(buffet);

            buffet = new BuffetTopModel();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Zafari Buffet";
            buffet.Category = BuffetCategory.Infantil;
            buffet.Thumbprint = "img/product2_2.jpg";

            buffets.Add(buffet);

            buffet = new BuffetTopModel();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Garotada Buffet";
            buffet.Category = BuffetCategory.Casamento;
            buffet.Thumbprint = "img/product1_2.jpg";

            buffets.Add(buffet);

            buffet = new BuffetTopModel();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Married Buffet";
            buffet.Category = BuffetCategory.Evento;
            buffet.Thumbprint = "img/product1_2.jpg";

            buffets.Add(buffet);

            return buffets;
        }
    }
}