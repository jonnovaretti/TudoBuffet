using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/dominio")]
    [ApiController]
    public class ValuesObjectsController : ControllerBase
    {
        private readonly IPlans plans;

        public ValuesObjectsController(IPlans plans)
        {
            this.plans = plans;
        }

        [Route("planos")]
        public ActionResult<List<Plan>> Get()
        {
            return Ok(plans.GetAll());
        }

        [Route("faixas-de-preco")]
        public ActionResult<List<RangePriceModel>> GetRangePrice()
        {
            List<RangePriceModel> rangesPriceModel;
            List<string> rangesPriceText;

            rangesPriceText = Enum.GetNames(typeof(RangePrice)).ToList();

            rangesPriceModel = new List<RangePriceModel>();

            foreach (var rangePriceText in rangesPriceText)
            {
                RangePriceModel rangePriceModel = null;

                rangePriceModel = RangePriceModel.CreateRangePriceModel(rangePriceText);

                rangesPriceModel.Add(rangePriceModel);
            }

            return Ok(rangesPriceModel);
        }

        [Route("categorias-buffet")]
        public ActionResult<List<BuffetCategoryModel>> GetBuffetCategory()
        {
            List<BuffetCategoryModel> buffetCategoriesModel;
            List<string> categoriesText;

            categoriesText = Enum.GetNames(typeof(BuffetCategory)).ToList();

            buffetCategoriesModel = new List<BuffetCategoryModel>();

            foreach (var categoryText in categoriesText)
            {
                buffetCategoriesModel.Add(new BuffetCategoryModel() { Code = categoryText, Text = categoryText });
            }

            return Ok(buffetCategoriesModel);
        }

        [Route("ambientes")]
        public ActionResult<List<EnvironmentModel>> GetEnvironments()
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

            return Ok(environmentsModel);
        }
    }
}