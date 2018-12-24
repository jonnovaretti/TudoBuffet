using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Infrastructures.Contracts;

namespace TudoBuffet.Website.Infrastructures
{
    public class IpLocalizator : IIpLocalizator
    {
        private readonly IOptions<ApplicationSetting> applicationSettings;
        private static HttpClient httpClient;
        
        public IpLocalizator(IOptions<ApplicationSetting> applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        public async Task<GeoLocation> GetCountryFromIp(string ip)
        {
            JObject jObject;
            string city = string.Empty, state = string.Empty, body;

            body = await RequestLocationFromIp(ip);

            if (body.Contains("success"))
            {
                jObject = JObject.Parse(body);

                city = jObject.SelectToken("data.geo.city").Value<string>();
                state = jObject.SelectToken("data.geo.region_code").Value<string>();
            }

            return new GeoLocation(city, state);
        }

        private async Task<string> RequestLocationFromIp(string ip)
        {
            HttpResponseMessage response;
            string body = string.Empty, uri;

            uri = string.Format(applicationSettings.Value.IpLocationUrl, ip);

            CreateHttpClient();
            response = await httpClient.GetAsync(uri);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                body = await response.Content.ReadAsStringAsync();

            return body;
        }

        private static void CreateHttpClient()
        {
            if (httpClient == null)
                httpClient = new HttpClient();
        }
    }

    public class GeoLocation
    {
        public string City { get; }
        public string State { get; }
        
        public GeoLocation(string city, string state)
        {
            City = city;
            State = state;
        }
    }
}
