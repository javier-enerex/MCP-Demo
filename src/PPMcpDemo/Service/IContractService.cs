using PPMcpDemo.Models;

namespace PPMcpDemo.Service;

public interface IContractService
{
    Task<PagedResult<VMContract>> GetContractAsync(VMContractFilter vMContractFilter, CancellationToken cancellationToken = default);
    Task<IEnumerable<VMContractTemplate>> GetContractTemplateAsync(VMContractTemplateFilter filter, CancellationToken cancellationToken = default);
    Task<string> CreateAndSendContractToSign(VMAddContractForSignatureRequest vMAddContractForSignatureRequest, CancellationToken cancellationToken = default);
}
