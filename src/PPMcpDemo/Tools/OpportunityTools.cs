using ModelContextProtocol.Server;
using PPMcpDemo.Models;
using PPMcpDemo.Service;
using System.ComponentModel;
using System.Text.Json;
// Removed incorrect using directive for ModelContextProtocol.Server.Attributes

namespace PPMcpDemo.Tools;

[McpServerToolType]
public class OpportunityTools(IOpportunityService opportunityService, ILogger<OpportunityTools> logger)
{
    /*[McpServerTool(Name = "get_opportunities", Title = "Get a list of opportunity items")]
    [Description("Gets a list of opportunities items from database.")]
    public async Task<IEnumerable<string>> GetOpportunityItemsAsync()
    {
        var opportunities = opportunityService.GetOpportunitiesMock();

        logger.LogInformation("Retrieved {Count} opportunity items.", opportunities.Count());

        return opportunities.Any()
               ? opportunities.Select(p => $"Text: {p.Name}")
               : ["No opportunity items found."];
    }*/

    //[McpServerTool(Name = "get_opportunities_by_name_status", Title = "Get a list of opportunity items from name and status")]
    //[Description("Gets a list of opportunities items from database by name and status.")]
    /*[McpServerTool(
    Name = "get_opportunities_by_filter",
    Title = "Retrieve filtered and paginated list of opportunities",
    UseStructuredContent = true
)]
[Description(
    "Retrieves a paginated list of opportunity items based on filters such as " +
    "name, customer name(s), salesperson(s), status, expected start date, created date range, " +
    "and service type(s). Supports pagination (Page, PageSize) and returns summarized fields to avoid payload size issues."
)]
    public async Task<IEnumerable<VMOpportunity>> GetOpportunityByNameAsync(VMOpportunityFilter filter)
    {
        var pageSize = Math.Min(filter.PageSize, 500);
    var skip = (filter.Page - 1) * pageSize;

    var opportunities = await opportunityService
        .GetOpportunitiesAsync(filter)
        .ContinueWith(t => t.Result.Skip(skip).Take(pageSize).ToList());

    logger.LogInformation("Returned {Count} opportunities (Page {Page}).",
        opportunities.Count, filter.Page);

    return opportunities;

    }*/
    [McpServerTool(Name = "get_opportunities_by_filter", Title = "Retrieve filtered and paginated list of opportunities", UseStructuredContent = true)]
    [Description("Retrieves a paginated list of opportunity items based on filters such as " +  "name, customer name(s), salesperson(s), status, expected start date, created date range, " +  "and service type(s). Supports cursor-based pagination via NextCursor and PageSize with default value 500. Also get total of opportunities via Total")]
    public async Task<string> GetOpportunityByNameAsync(VMOpportunityFilter filter)
    {
        var query = await opportunityService.GetOpportunitiesAsync(filter);

        logger.LogInformation("Returned {Count} opportunities (NextCursor={NextCursor}).", query.Items.Count(), query.NextCursor);

        return JsonSerializer.Serialize(query);
    }
}
