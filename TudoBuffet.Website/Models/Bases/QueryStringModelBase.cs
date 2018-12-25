using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TudoBuffet.Website.Models.Bases
{
    public class QueryStringModelBase : IQueryStringModel
    {
        public string Code { get; set; }

        public string QueryString { get; set; }

        public void FormatQueryString(IQueryStringModel queryStringModel, string paramName, string currentQueryString)
        {
            if (currentQueryString.Contains(paramName))
            {
                string valueQueryString, queryString;

                valueQueryString = GetValueFromParam(currentQueryString, paramName);
                queryString = currentQueryString.Replace(valueQueryString, queryStringModel.Code);

                queryStringModel.QueryString = queryString;
            }
            else
            {
                if (currentQueryString.Contains('?'))
                    queryStringModel.QueryString = string.Concat(currentQueryString.Value, "&", paramName, "=", queryStringModel.Code);
                else
                    queryStringModel.QueryString = string.Concat("?", paramName, "=", queryStringModel.Code);
            }
        }

        private string GetValueFromParam(string currentQueryString, string paramName)
        {
            string value;

            value = currentQueryString.Remove(0, currentQueryString.IndexOf(paramName));

            if (value.Contains("&"))
                value = value.Remove(value.IndexOf("&"));

            return value;
        }
    }
}
