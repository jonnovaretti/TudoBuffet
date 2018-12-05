namespace TudoBuffet.Website.Models
{
    public class EnvironmentModel
    {
        public string Text { get; private set; }
        public string Code { get; private set; }

        public EnvironmentModel(string code, string text)
        {
            Code = code;
            Text = text;
        }

        public static EnvironmentModel CreateEnvironmentModel(string environmentText)
        { 
            EnvironmentModel environmentModel = null;

            switch (environmentText)
            {
                case "SalaoDeFesta":
                    environmentModel = new EnvironmentModel("SalaoDeFesta", "Salão de festa" );
                    break;
                case "Fazenda":
                    environmentModel = new EnvironmentModel("Fazenda", "Fazenda" );
                    break;
                case "Clube":
                    environmentModel = new EnvironmentModel("Clube", "Clube" );
                    break;
                case "Restaurante":
                    environmentModel = new EnvironmentModel("Restaurante", "Restaurante" );
                    break;
                case "AreaDeEntretenimento":
                    environmentModel = new EnvironmentModel("AreaDeEntretenimento", "Área de entretenimento" );
                    break;
                case "Praia":
                    environmentModel = new EnvironmentModel("Praia", "Praia");
                    break;
                default:
                    break;
            }

            return environmentModel;
        }
    }
}