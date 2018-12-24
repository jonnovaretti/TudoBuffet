using System.Threading.Tasks;

namespace TudoBuffet.Website.Infrastructures.Contracts
{
    public interface IIpLocalizator
    {
        Task<GeoLocation> GetCountryFromIp(string ip);
    }
}
