using System.ComponentModel;

namespace TudoBuffet.Website.ValuesObjects
{
    public enum RangePrice
    {
        [Description("Menos de R$ 2000,00")]
        Menos2000,
        [Description("Entre R$ 2000,00 e R$ 4000,00")]
        Entre2000e4000,
        [Description("Entre R$ 4000,00 e R$ 6000,00")]
        Entre4000e6000,
        [Description("Entre R$ 6000,00 e R$ 8000,00")]
        Entre6000e8000,
        [Description("Entre R$ 8000,00 e R$ 12000,00")]
        Entre8000e12000,
        [Description("Mais do que R$ 12000,00")]
        Mais12000
    }
}
