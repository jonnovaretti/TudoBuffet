﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/buffets")]
    [AllowAnonymous]
    [ApiController]
    public class BuffetController : ControllerBase
    {
        [Route("destaques")]
        public ActionResult<List<Buffet>> GetHotest()
        {
            List<Buffet> buffets = new List<Buffet>();
            Buffet buffet = new Buffet();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Afro Festa";
            buffet.Category = BuffetCategory.Infantil;
            buffet.Thumbprint = "img/product3_2.jpg";

            buffets.Add(buffet);

            buffet = new Buffet();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Zafari Buffet";
            buffet.Category = BuffetCategory.Infantil;
            buffet.Thumbprint = "img/product2_2.jpg";

            buffets.Add(buffet);

            buffet = new Buffet();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Garotada Buffet";
            buffet.Category = BuffetCategory.Casamento;
            buffet.Thumbprint = "img/product1_2.jpg";

            buffets.Add(buffet);

            buffet = new Buffet();

            buffet.Id = Guid.NewGuid();
            buffet.Name = "Married Buffet";
            buffet.Category = BuffetCategory.Evento;
            buffet.Thumbprint = "img/product1_2.jpg";

            buffets.Add(buffet);

            return buffets;
        }
    }
}