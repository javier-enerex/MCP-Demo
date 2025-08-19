using System.Web;
using PPMcpDemo.Models;

public static class QueryStringHelper
{
    public static string ToQueryString(VMOpportunityFilter filter)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query["Name"] = filter.Name;

        if (filter.CustomerName?.Any() == true)
            foreach (var c in filter.CustomerName)
                query.Add("CustomerName", c);

        if (filter.SalesPerson?.Any() == true)
            foreach (var s in filter.SalesPerson)
                query.Add("SalesPerson", s);

        if (filter.Status?.Any() == true)
            foreach (var st in filter.Status)
                query.Add("Status", st);

        if (filter.ExpectedStartDate.HasValue)
            query["ExpectedStartDate"] = filter.ExpectedStartDate.Value.ToString("o");

        if (filter.CreatedDateFrom.HasValue)
            query["CreatedDateFrom"] = filter.CreatedDateFrom.Value.ToString("o");

        if (filter.CreatedDateTo.HasValue)
            query["CreatedDateTo"] = filter.CreatedDateTo.Value.ToString("o");

        if (filter.ServiceType?.Any() == true)
            foreach (var s in filter.ServiceType)
                query.Add("ServiceType", s);

        return query.ToString();
    }
}
