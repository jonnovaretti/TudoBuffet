using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Models.Bases;
using TudoBuffet.Website.ValuesObjects;

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

        public static RangePriceModel Create<T>(T type) where T :struct
        {
            return new RangePriceModel(Enum.GetName(typeof(T), type), type.GetDescription());
        }
        
        public static List<RangePriceModel> GetRangePriceList()
        {
            List<RangePriceModel> rangesPriceModel;
            IEnumerable<RangePrice> rangesPrices;

            rangesPrices = Enum.GetValues(typeof(RangePrice)).Cast<RangePrice>();

            rangesPriceModel = new List<RangePriceModel>();

            foreach (var rangePrice in rangesPrices)
            {
                rangesPriceModel.Add(new RangePriceModel(Enum.GetName(typeof(RangePrice), rangePrice), rangePrice.GetDescription()));
            }
            
            return rangesPriceModel;
        }
    }
}
