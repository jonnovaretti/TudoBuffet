using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Models.Bases;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Models
{
    public class BuffetCategoryModel : QueryStringModelBase
    {
        public string Text { get; set; }

        public static List<BuffetCategoryModel> GetBuffetCategories()
        {
            List<BuffetCategoryModel> buffetCategoriesModel;
            List<string> categoriesText;

            categoriesText = Enum.GetNames(typeof(BuffetCategory)).ToList();

            buffetCategoriesModel = new List<BuffetCategoryModel>();

            foreach (var categoryText in categoriesText)
            {
                buffetCategoriesModel.Add(new BuffetCategoryModel() { Code = categoryText, Text = categoryText });
            }

            return buffetCategoriesModel;
        }
    }
}