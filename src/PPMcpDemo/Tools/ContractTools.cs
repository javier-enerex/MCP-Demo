using ModelContextProtocol.Server;
using PPMcpDemo.Models;
using PPMcpDemo.Service;
using System.ComponentModel;
using System.Text.Json;

namespace PPMcpDemo.Tools;

[McpServerToolType]
public class ContractTools(IContractService contractService, ILogger<ContractTools> logger)
{
    [McpServerTool(Name = "get_contract_by_filter", Title = "Retrieve filtered and paginated list of contracts", UseStructuredContent = true)]
    [Description(
        "Retrieves a paginated list of contracts based on filters such as salesperson IDs, " +
        "signature method, sign date, status IDs, service type IDs, and contract name. " +
        "Supports cursor-based pagination via NextCursor and a PageSize (default: 500). " +
        "Also returns the total number of matching contracts in the Total field."
    )]
    public async Task<string> GetContractByFiltersAsync(VMContractFilter filter)
    {
        var query = await contractService.GetContractAsync(filter);

        logger.LogInformation("Returned {Count} contract (NextCursor={NextCursor}).", query.Total, query.NextCursor);

        return JsonSerializer.Serialize(query);
    }

    [McpServerTool(Name = "get_contract_template_by_filter", Title = "Retrieve filtered list of contract templates", UseStructuredContent = true)]
    [Description(
        "Retrieves a list of contract templates based on filters such as opportunity external ID, " +
        "custom pricing external ID, custom pricing scenario external ID and template name. The opportunity external ID is required."
    )]
    public async Task<string> GetContractTemplateByFiltersAsync(VMContractTemplateFilter filter)
    {
        var query = await contractService.GetContractTemplateAsync(filter);

        logger.LogInformation("Returned {Count} contract template(s).", query.Count());

        return JsonSerializer.Serialize(query);
    }

    [McpServerTool(Name = "send_contract_for_signature", Title = "Create & send contract for e-signature", UseStructuredContent = true)]
    [Description(
        "Creates a contract from a template and sends it for e-signature. " +
        "Requires OpportunityExternalID, ContractTemplateID, " +
        "CustomPricingScenarioExternalID, ToEmails, SupplierEmail. " +
        "CCEmails is optional."
    )]
    public async Task<string> SendContractForSignatureAsync(VMAddContractForSignatureRequest payload, CancellationToken cancellationToken = default)
    {
        if (payload is null) throw new ArgumentNullException(nameof(payload));
        if (payload.OpportunityExternalID == Guid.Empty) throw new ArgumentException("OpportunityExternalID is required.");
        if (payload.CustomPricingScenarioExternalID == Guid.Empty) throw new ArgumentException("CustomPricingScenarioExternalID is required.");

        if (!EmailValidator.IsValid(payload.SupplierEmail))
        {
            throw new ArgumentException("Supplier Email must contain a valid email.");
        }

        if (!EmailValidator.AreAllValid(payload.CCEmails))
        {
            EmailValidator.TryGetInvalids(payload.CCEmails, out var bad);
        }

        long templateId = payload.ContractTemplateID;
        if (templateId <= 0)
        {
            var filter = new VMContractTemplateFilter
            {
                OpportunityExternalID = payload.OpportunityExternalID,
                CustomPricingScenarioExternalID = payload.CustomPricingScenarioExternalID
            };

            var templates = await contractService.GetContractTemplateAsync(filter, cancellationToken);
            var match = templates
                .OrderByDescending(t => t.TemplateName)
                .ThenByDescending(t => t.Selected)
                .ThenBy(t => t.OrderBy)
                .FirstOrDefault();

            if (match is null)
                throw new InvalidOperationException($"No contract template found matching for the provided opportunity/scenario.");

            templateId = match.ID;
        }

        var response = await contractService.CreateAndSendContractToSign(payload with { ContractTemplateID = templateId }, cancellationToken);
        
        return JsonSerializer.Serialize(response);
    }
}
