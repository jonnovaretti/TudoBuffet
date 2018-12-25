using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models.Bases;

namespace TudoBuffet.Website.Models
{
    public class RangePriceModel : QueryStringModelBase
    {
        public string Text { get; private set; }

        private RangePriceModel(string code, string text)
        {
            Code = code;
            Text = text;
        }
        
        public static List<RangePriceModel> GetRangePriceList()
        {
            List<RangePriceModel> rangesPriceModel;
            List<string> rangesPriceText;

            rangesPriceText = Enum.GetNames(typeof(RangePrice)).ToList();

            rangesPriceModel = new List<RangePriceModel>();

            foreach (var rangePriceText in rangesPriceText)
            {
                RangePriceModel rangePriceModel = null;

                rangePriceModel = CreateRangePriceModel(rangePriceText);

                rangesPriceModel.Add(rangePriceModel);
            }

            return rangesPriceModel;
        }

        public static RangePriceModel CreateRangePriceModel(string rangePriceText)
        {
            RangePriceModel rangePriceModel = null;

            switch (rangePriceText)
            {
                case "Less2000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Menos de R$ 2000,00");
                    break;
                case "Between2000And4000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Entre R$ 2000,00 e R$ 4000,00");
                    break;
                case "Between4000And6000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Entre R$ 4000,00 e R$ 6000,00");
                    break;
                case "Between6000And8000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Entre R$ 6000,00 e R$ 8000,00");
                    break;
                case "Between8000And12000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Entre R$ 8000,00 e R$ 12000,00");
                    break;
                case "More12000":
                    rangePriceModel = new RangePriceModel(rangePriceText, "Mais do que R$ 12000,00");
                    break;
            }

            return rangePriceModel;
        }
    }
}
