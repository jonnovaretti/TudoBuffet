using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Infrastructures.Interfaces;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("usuarios")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserSignup userSignupService;
        private readonly IUserAuthenticatior userAuthenticatiorService;
        private readonly IHttpContextAccessor httpContext;
        private readonly IRecaptchaValidator recaptchaValidator;

        public UserController(IUserSignup userSignupService, IUserAuthenticatior userAuthenticatiorService, IHttpContextAccessor httpContext, IRecaptchaValidator recaptchaValidator)
        {
            this.userSignupService = userSignupService;
            this.userAuthenticatiorService = userAuthenticatiorService;
            this.httpContext = httpContext;
            this.recaptchaValidator = recaptchaValidator;
        }

        [Route("registrar")]
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [Route("registrar")]
        public IActionResult RegisterUser(UserModel userModel)
        {
            User user;
            string clientIp, recaptchaResponse;

            try
            {
                clientIp = httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                recaptchaResponse = Request.Form["g-recaptcha-response"];

                if (!recaptchaValidator.IsRecaptchaValid(recaptchaResponse, clientIp))
                    throw new ArgumentException("Recaptcha inválido");

                if (userModel.Password != userModel.ConfirmationPassword)
                    throw new BusinessException("As senhas não correspondem");

                if (userModel.Password.Length < 4)
                    throw new BusinessException("Senha deve conter ao menos 4 caracteres");

                user = userModel.ToEntity();

                userSignupService.RegisterNewUser(user);

                return View("Foi enviado um e-mail de confirmação. Siga as instruções para conseguir fazer seu anuncio.");
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
                    AuthenticatedUserModel authenticatedUser = userAuthenticatiorService.GenerateJwt(userModel.Email);

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