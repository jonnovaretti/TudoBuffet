using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Infrastructures.Contracts;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Controllers
{
    [Route("usuarios")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserAccount userSignupService;
        private readonly IHttpContextAccessor httpContext;
        private readonly IRecaptchaValidator recaptchaValidator;

        public UserController(IUserAccount userSignupService, IHttpContextAccessor httpContext, IRecaptchaValidator recaptchaValidator)
        {
            this.userSignupService = userSignupService;
            this.httpContext = httpContext;
            this.recaptchaValidator = recaptchaValidator;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Index(UserModel userModel)
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

                return View("ReturnRegisterUser");
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

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View(new UserModel());
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserModel userModel)
        {
            try
            {
                ClaimsIdentity claimsGenerated;

                claimsGenerated = userSignupService.AutheticateUser(userModel);

                if (claimsGenerated == null)
                    return NotFound("E-mail ou senha inválidos");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsGenerated), new AuthenticationProperties()).GetAwaiter().GetResult();

                if (GetRoleClaimValue(claimsGenerated).Equals(Enum.GetName(typeof(Profile), Profile.UserBuffetAdmin)))
                    return RedirectToAction("Index", "Admin");

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private static string GetRoleClaimValue(ClaimsIdentity claimsGenerated)
        {
            return claimsGenerated.Claims.ToList().Where(c => c.Type == claimsGenerated.RoleClaimType).First().Value;
        }
    }
}