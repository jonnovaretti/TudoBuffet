using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TudoBuffet.Website.Models
{
    public class FileUploadModel
    {
        [Display(Name = "files")]
        public IFormFile Files { get; set; }
    }
}
