using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetCategoryModel
    {
        public string Text { get; set; }
        public string Code { get; set; }

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