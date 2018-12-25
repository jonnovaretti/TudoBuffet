using System;
using System.Linq;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstThumbnailUrl { get; set; }
        public string SecondThumbnailUrl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Category { get; set; }

        public static BuffetSearchModel ToModel(Buffet buffet)
        {
            var buffetFoundModel = new BuffetSearchModel()
            {
                City = buffet.City,
                Id = buffet.Id,
                Name = buffet.Name,
                State = buffet.State,
                FirstThumbnailUrl = buffet.Photos.Any() ? buffet.Photos.First().SearchUrl : string.Empty,
                SecondThumbnailUrl = buffet.Photos.Any() ? buffet.Photos.Last().SearchUrl : string.Empty,
                Category = buffet.Category.ToString()
            };

            return buffetFoundModel;
        }
    }
}
