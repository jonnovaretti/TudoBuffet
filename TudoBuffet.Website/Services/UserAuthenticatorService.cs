using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.Tools;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Services
{
    public class UserAuthenticatorService : IUserAuthenticatior
    {
        private readonly MainDbContext mainDbContext;
        private readonly IOptions<ApplicationSetting> appSettings;

        public UserAuthenticatorService(MainDbContext mainDbContext, IOptions<ApplicationSetting> appSettings)
        {
            this.mainDbContext = mainDbContext;
            this.appSettings = appSettings;
        }

        

        public AuthenticatedUserModel GenerateCookie(User user)
        {

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new );
        }

        public AuthenticatedUserModel GenerateJwt(string email)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityTokenDescriptor securityTokenConfiguration;
            SecurityToken token;
            User user;
            string tokenText;
            byte[] key;

            user = mainDbContext.Users.FirstOrDefault(u => u.Email == email);

            tokenHandler = new JwtSecurityTokenHandler();
            key = Encoding.ASCII.GetBytes(appSettings.Value.SecretKey);

            securityTokenConfiguration = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            token = tokenHandler.CreateToken(securityTokenConfiguration);
            tokenText = tokenHandler.WriteToken(token);

            return new AuthenticatedUserModel { Token = tokenText, Id = user.Id };
        }

        public User GetUserById(Guid id)
        {
            return mainDbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
