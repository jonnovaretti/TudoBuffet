namespace TudoBuffet.Website.Infrastructures.Contracts
{
    public interface IRecaptchaValidator
    {
        bool IsRecaptchaValid(string recaptchaKeyReceived, string clientIp);
    }
}