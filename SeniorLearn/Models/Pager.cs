using cloudscribe.Pagination.Models;

namespace SeniorLearn.Models;

public interface IPager

{
    string Controller { get; set;}
    string Action { get; set; }
    int PageSize { get; set; }
    long PageNumber { get; set; }
    long TotalItems { get; set; }
}

public class Pager : IPager
{
    public string Controller { get; set; } = default!;
    public string Action { get; set; } = default!;
    public int PageSize { get; set; }
    public long PageNumber { get; set; }
    public long TotalItems { get; set; }
    public Pager(string controller, string action, dynamic args)
    {
        Controller = controller;
        Action = action;
        PageSize = args.PageSize;
        PageNumber = args.PageNumber;
        TotalItems = args.TotalItems;
    }
}
public static class PagerExtensions
{
    public static IPager GetPagerModel<T>(this PagedResult<T> t, string controller, string action) where T : class => new Pager(controller, action, new { t.PageNumber, t.PageSize, t.TotalItems });
}
