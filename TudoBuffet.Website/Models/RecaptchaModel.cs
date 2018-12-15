namespace TudoBuffet.Website.Models
{
    public class RecaptchaModel
    {
        public string Secret { get; set; }
        public string Response { get; set; }
        public string RemoteIp { get; set; }
    }
}
