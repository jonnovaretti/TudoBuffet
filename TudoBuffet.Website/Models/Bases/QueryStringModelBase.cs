namespace TudoBuffet.Website.Models.Bases
{
    public class QueryStringModelBase : IQueryStringModel
    {
        public string Code { get; set; }

        public string QueryString { get; set; }
    }

    public static class QueryStringExtensions
    {
        public static void FormatQueryString(this IQueryStringModel queryStringModel, string paramName, string currentQueryString)
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
                    queryStringModel.QueryString = string.Concat(currentQueryString, "&", paramName, "=", queryStringModel.Code);
                else
                    queryStringModel.QueryString = string.Concat("?", paramName, "=", queryStringModel.Code);
            }
        }

        private static string GetValueFromParam(string currentQueryString, string paramName)
        {
            string value;

            value = currentQueryString.Remove(0, currentQueryString.IndexOf(paramName) + paramName.Length + 1);

            if (value.Contains("&"))
                value = value.Remove(value.IndexOf("&"));

            return value;
        }
    }
}
