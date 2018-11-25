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
                switch (rangePriceText)
                {
                    case "Less1000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Menos de R$ 2000,00" });
                        break;
                    case "Between2000And4000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Entre R$ 2000,00 e R$ 4000,00" });
                        break;
                    case "Between4000And6000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Entre R$ 4000,00 e R$ 6000,00" });
                        break;
                    case "Between6000And8000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Entre R$ 6000,00 e R$ 8000,00" });
                        break;
                    case "Between8000And12000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Entre R$ 8000,00 e R$ 12000,00" });
                        break;
                    case "More12000":
                        rangesPriceModel.Add(new RangePriceModel() { Code = rangePriceText, Text = "Mais do que R$ 12000,00" });
                        break;
                    default:
                        break;
                }
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
                switch (environmentText)
                {
                    case "SalaoDeFesta":
                        environmentsModel.Add(new EnvironmentModel() { Code = "SalaoDeFesta", Text = "Salão de festa" });
                        break;
                    case "Fazenda":
                        environmentsModel.Add(new EnvironmentModel() { Code = "Fazenda", Text = "Fazenda" });
                        break;
                    case "Clube":
                        environmentsModel.Add(new EnvironmentModel() { Code = "Clube", Text = "Clube" });
                        break;
                    case "Restaurante":
                        environmentsModel.Add(new EnvironmentModel() { Code = "Restaurante", Text = "Restaurante" });
                        break;
                    case "AreaDeEntretenimento":
                        environmentsModel.Add(new EnvironmentModel() { Code = "AreaDeEntretenimento", Text = "Área de entretenimento" });
                        break;
                    case "Praia":
                        environmentsModel.Add(new EnvironmentModel() { Code = "Praia", Text = "Praia" });
                        break;
                    default:
                        break;
                }
            }

            return Ok(environmentsModel);
        }
    }
}