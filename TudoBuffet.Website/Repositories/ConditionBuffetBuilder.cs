using System.Collections.Generic;
using System.Text;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Repositories
{
    public class ConditionBuffetBuilder
    {
        private int paramsOrder;
        private readonly List<object> paramsValue;
        private readonly StringBuilder where;

        public ConditionBuffetBuilder()
        {
            paramsOrder = 0;
            where = new StringBuilder("1=1");
            paramsValue = new List<object>();
        }

        public ConditionBuffetBuilder WhereUf(string uf)
        {
            if (!string.IsNullOrEmpty(uf))
            {
                where.Append(" and ");
                where.Append(" state == @" + paramsOrder);
                paramsValue.Add(uf);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder WhereCity(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                where.Append(" and ");
                where.Append(" city == @" + paramsOrder);
                paramsValue.Add(city);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder WhereCategory(BuffetCategory? buffetCategory)
        {
            if (buffetCategory.HasValue)
            {
                where.Append(" and ");
                where.Append(" category == @" + paramsOrder);
                paramsValue.Add(buffetCategory);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder WhereEnvironment(BuffetEnvironment? buffetEnvironment)
        {
            if (buffetEnvironment.HasValue)
            {
                where.Append(" and ");
                where.Append(" environment = @" + paramsOrder);
                paramsValue.Add(buffetEnvironment);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder WhereRangePrice(RangePrice? rangePrice)
        {
            if (rangePrice.HasValue)
            {
                where.Append(" and ");
                where.Append(" price = @" + paramsOrder);
                paramsValue.Add(rangePrice);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder WhereName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                where.Append(" and ");
                where.Append(" name.Contains(@" + paramsOrder + ")");
                paramsValue.Add(name);
                paramsOrder++;
            }

            return this;
        }

        public ConditionBuffetBuilder Build()
        {
            return this;
        }

        public string GetWhere()
        {
            return where.ToString();
        }

        public object[] GetParams()
        {
            return paramsValue.ToArray();
        }
    }
}