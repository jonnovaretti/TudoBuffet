using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/usuarios")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSignup userSignupService;
        private readonly IUserAuthenticatior userAuthenticatiorService;

        public UserController(IUserSignup userSignupService, IUserAuthenticatior userAuthenticatiorService)
        {
            this.userSignupService = userSignupService;
            this.userAuthenticatiorService = userAuthenticatiorService;
        }

        public ActionResult Post(UserModel userModel)
        {
            User user;

            try
            {
                if (userModel.Password != userModel.ConfirmationPassword)
                    throw new BusinessException("As senhas não correspondem");

                user = userModel.ToEntity();

                userSignupService.RegisterNewUser(user);

                return Ok("Foi enviado um e-mail de confirmação. Siga as instruções para conseguir fazer seu anuncio.");
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("entrar")]
        public ActionResult Authenticate(UserModel userModel)
        {
            try
            {
                if (userAuthenticatiorService.IsCredentialCorrect(userModel.Email, userModel.Password))
                {
                    AuthenticatedUser authenticatedUser = userAuthenticatiorService.GenerateJwt(userModel.Email);

                    return Ok(new { authenticatedUser });
                }

                return NotFound("E-mail ou senha inválidos");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}