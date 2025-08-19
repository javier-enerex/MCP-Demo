using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public sealed record VMAddContractForSignatureRequest
{
    [Description("Opportunity external ID associated with the contract. This is resolved from opportunity tools. Required.")]
    [JsonPropertyName("OpportunityExternalID")]
    [Required]
    public Guid OpportunityExternalID { get; init; }

    [Description("Contract template ID. This is resolved from contract template tools. If there are multiple templates available, you must ask for the specific template ID. Required.")]
    [JsonPropertyName("ContractTemplateID")]
    [Required]
    public long ContractTemplateID { get; init; }

    [Description("Custom pricing scenario external ID. Required. This is used to resolve the custom pricing scenario in opportunity tools.")]
    [JsonPropertyName("CustomPricingScenarioExternalID")]
    [Required]
    public Guid CustomPricingScenarioExternalID { get; init; }

    [Description("Recipient email(s). Accepts 'a@b.com', 'a@b.com, or b@c.com'. This is resolved from opportunity customer email. If there are not customer email in the opportunity you must ask for the customer email. Required.")]
    [JsonPropertyName("CustomerEmail")]
    [Required, MinLength(1)]
    public string CustomerEmail { get; init; } = string.Empty;

    [Description("Supplier email(s). Accepts 'a@b.com', 'a@b.com, or b@c.com'. Required.")]
    [JsonPropertyName("SupplierEmail")]
    [Required, MinLength(1)]
    public string SupplierEmail { get; init; } = string.Empty;

    [Description("Optional CC list. Accepts comma/semicolon/space-separated emails. Normalized to 'a@b.com;b@c.com'.")]
    [JsonPropertyName("CCEmails")]
    public string? CCEmails { get; init; }
}
