using Microsoft.AspNetCore.Mvc;
using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSignupService userService;

        public UserController(IUserSignupService userService)
        {
            this.userService = userService;
        }

        public ActionResult Post(UserRegisterModel userModel)
        {
            User user;

            if (userModel.Password != userModel.ConfirmationPassword)
                throw new FieldAccessException("As senhas não correspondem");

            user = userModel.ToEntity();

            userService.RegisterNewUser(user);

            return Ok();
        }
    }
}