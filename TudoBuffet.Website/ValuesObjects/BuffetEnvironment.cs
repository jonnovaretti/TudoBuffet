using System.ComponentModel;

namespace TudoBuffet.Website.ValuesObjects
{
    public enum BuffetEnvironment
    {
        [Description("Salão de festa")]
        SalaoDeFesta,
        [Description("Fazenda")]
        Fazenda,
        [Description("Clube")]
        Clube,
        [Description("Restaurante")]
        Restaurante,
        [Description("Área de entretenimento")]
        AreaDeEntretenimento,
        [Description("Praia")]
        Praia,
        [Description("Sítio/chácara")]
        SitioChacara
    }
}
