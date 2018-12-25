using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models.Bases;

namespace TudoBuffet.Website.Models
{
    public class EnvironmentModel : QueryStringModelBase
    {
        public string Text { get; private set; }

        public EnvironmentModel(string code, string text)
        {
            Code = code;
            Text = text;
        }

        public static List<EnvironmentModel> GetEnvironments()
        {
            List<EnvironmentModel> environmentsModel;
            List<string> environments;

            environments = Enum.GetNames(typeof(BuffetEnvironment)).ToList();

            environmentsModel = new List<EnvironmentModel>();

            foreach (var environmentText in environments)
            {
                EnvironmentModel environmentModel = null;

                environmentModel = EnvironmentModel.CreateEnvironmentModel(environmentText);

                environmentsModel.Add(environmentModel);
            }

            return environmentsModel;
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
                case "SitioChacara":
                    environmentModel = new EnvironmentModel("SitioCharaca", "Sitio/chacara");
                    break;
                default:
                    break;
            }

            return environmentModel;
        }
    }
}