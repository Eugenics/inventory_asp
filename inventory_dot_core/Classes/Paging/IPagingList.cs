using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace inventory_dot_core.Classes.Paging
{
    public interface IPagingList
    {
        string Action { get; set; }
        RouteValueDictionary GetRouteValueForPage(int pageIndex);
        int PageCount { get; }
        int PageIndex { get; }
        int TotalRecordCount { get; }
        RouteValueDictionary RouteValue { get; set; }
        string SortExpression { get; }

        int NumberOfPagesToShow { get; set; }
        int StartPageIndex { get; }
        int StopPageIndex { get; }
    }
}
