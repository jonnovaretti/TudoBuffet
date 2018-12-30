using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Models
{
    public class BuffetDetailModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> PhotosUrls { get; set; }
        public string Location { get; set; }
        public List<string> ThumbnailsUrls { get; set; }
        public string RangePrince { get; internal set; }
        public string EnvironmentType { get; internal set; }
        public Guid Id { get; set; }

        public static BuffetDetailModel Create(Buffet buffetFound, RangePriceModel rangePriceModel, EnvironmentModel environmentModel)
        {
            return new BuffetDetailModel()
            {
                Name = buffetFound.Name,
                Category = Enum.GetName(typeof(BuffetCategory), buffetFound.Category),
                Description = buffetFound.Description,
                Location = string.Concat(buffetFound.Street, ", ", buffetFound.Number, ", ", buffetFound.District, " - ", buffetFound.City, "-", buffetFound.State),
                RangePrince = rangePriceModel.Text,
                EnvironmentType = environmentModel.Text,
                PhotosUrls = buffetFound.Photos.Select(p => p.DetailUrl).ToList(),
                ThumbnailsUrls = buffetFound.Photos.Select(p => p.ThumbnailUrl).ToList(),
                Id = buffetFound.Id
            };
        }
    }
}
