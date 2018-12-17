using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class BuffetAdminViewModel
    {
        public List<PurchasedBuffetAdModel> PurchasedBuffetAds { get; set; }
        public string OwnerName { get; internal set; }
    }
}
