using Microsoft.AspNetCore.Mvc;

namespace TudoBuffet.Website.Models
{
    public class FilterBuffetSearch : PagingBase
    {
        [FromQuery(Name = "ambiente")]
        public string Environment { get; set; }
        [FromQuery(Name = "categoria")]
        public string Category { get; set; }
        [FromQuery(Name = "faixadepreco")]
        public string RangePrice { get; set; }
        [FromQuery(Name = "uf")]
        public string State { get; set; }
        [FromQuery(Name = "cidade")]
        public string City { get; set; }
        [FromQuery(Name = "nome")]
        public string Name { get; set; }
        [FromQuery(Name = "pagina")]
        public int Page { get; set; }
        [FromQuery(Name = "itens")]
        public int PageSize { get; set; }
    }
}