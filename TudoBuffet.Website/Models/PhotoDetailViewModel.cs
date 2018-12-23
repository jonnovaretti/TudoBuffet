using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class PhotoDetailViewModel
    {
        public List<PhotoUploadedModel> Photos { get; set; }
        public Guid BuffetId { get; set; }
    }
}
