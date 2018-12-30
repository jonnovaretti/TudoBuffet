using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Models.Bases;
using TudoBuffet.Website.ValuesObjects;

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

        public static EnvironmentModel Create<T>(T type) where T : struct
        {
            return new EnvironmentModel(Enum.GetName(typeof(T), type), type.GetDescription());
        }

        public static List<EnvironmentModel> GetEnvironments()
        {
            List<EnvironmentModel> environmentsModel;
            IEnumerable<BuffetEnvironment> environments;

            environments = Enum.GetValues(typeof(BuffetEnvironment)).Cast<BuffetEnvironment>();

            environmentsModel = new List<EnvironmentModel>();

            foreach (var environment in environments)
            {
                EnvironmentModel environmentModel = null;

                environmentModel = new EnvironmentModel(Enum.GetName(typeof(BuffetEnvironment), environment), environment.GetDescription());

                environmentsModel.Add(environmentModel);
            }

            return environmentsModel;
        }
    }
}