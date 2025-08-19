using PPMcpDemo.Models;
using System.Net.Http.Headers;

namespace PPMcpDemo.Service;

public class OpportunityService : IOpportunityService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    private static readonly List<VMOpportunity> _opportunityList = new()
    {
        new VMOpportunity { Name = "Opportunity A", StatusName = "Open", CustomerName = "Customer 1", CreatedAt = DateTime.UtcNow.AddDays(-10) },
        new VMOpportunity { Name = "Opportunity B", StatusName = "Closed Won", CustomerName = "Customer 2", CreatedAt = DateTime.UtcNow.AddDays(-20) },
        new VMOpportunity { Name = "Opportunity C", StatusName = "Open", CustomerName = "Customer 3", CreatedAt = DateTime.UtcNow.AddDays(-5) }
    };

    public OpportunityService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<PagedResult<VMOpportunity>> GetOpportunitiesAsync(VMOpportunityFilter vMOpportunityFilter)
    {

        /*var baseUrl = _config["OpportunityApi:BaseUrl"];
        var query = QueryStringHelper.ToQueryString(vMOpportunityFilter);

        var url = $"{baseUrl}/without-pagination?{query}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<VMOpportunity>>();*/

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_config["PropellerApi:BaseUrl"]}/opportunity/without-pagination")
        {
            Content = JsonContent.Create(vMOpportunityFilter)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PagedResult<VMOpportunity>>();
    }

    public IEnumerable<VMOpportunity> GetOpportunitiesMock()
    {
        return _opportunityList;
    }
}
