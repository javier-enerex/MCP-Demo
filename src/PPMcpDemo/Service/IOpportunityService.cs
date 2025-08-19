using PPMcpDemo.Models;

namespace PPMcpDemo.Service;

public interface IOpportunityService
{
    Task<PagedResult<VMOpportunity>> GetOpportunitiesAsync(VMOpportunityFilter vMOpportunityFilter);
    IEnumerable<VMOpportunity> GetOpportunitiesMock();
}
