namespace TudoBuffet.Website.Models.Bases
{
    public interface IQueryStringModel
    {
        string Code { get; }
        string QueryString { get; set; }
    }
}