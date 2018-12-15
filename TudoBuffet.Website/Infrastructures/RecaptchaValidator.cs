using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Infrastructures.Interfaces;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Infrastructures
{
    public class RecaptchaValidator : IRecaptchaValidator
    {
        private readonly IOptions<RecaptchaSetting> recaptchaSetting;

        public RecaptchaValidator(IOptions<RecaptchaSetting> recaptchaSetting)
        {
            this.recaptchaSetting = recaptchaSetting;
        }

        public bool IsRecaptchaValid(string recaptchaKeyReceived, string clientIp)
        {
            HttpClient httpClient;
            RecaptchaModel recaptchaModel;

            recaptchaModel = new RecaptchaModel
            {
                RemoteIp = httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                Response = Request.Form["g-recaptcha-response"],
                Secret = recaptchaSetting.Value.Key
            };

            httpClient = new HttpClient();
            var response = httpClient.PostAsJsonAsync(recaptchaSetting.Value.GoogleUrl, recaptchaModel).GetAwaiter().GetResult();

            return response.ReasonPhrase.Equals("OK");
        }
    }
}
