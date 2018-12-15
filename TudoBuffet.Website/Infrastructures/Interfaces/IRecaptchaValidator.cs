namespace TudoBuffet.Website.Infrastructures.Interfaces
{
    public interface IRecaptchaValidator
    {
        bool IsRecaptchaValid(string recaptchaKeyReceived, string clientIp);
    }
}