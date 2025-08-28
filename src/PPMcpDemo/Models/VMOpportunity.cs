using System.ComponentModel;

namespace PPMcpDemo.Models;

public class VMOpportunity
{
    [Description("Opportunitiy ID")]
    public long ID { get; set; }
    [Description("Opportunitiy external ID")]
    public Guid ExternalID { get; set; }
    [Description("Opportunitiy creation date")]
    public DateTime CreatedAt { get; set; }
    [Description("Opportunitiy name")]
    public string Name { get; set; }
    [Description("Opportunitiy status name")]
    public string StatusName { get; set; }
    [Description("Opportunitiy sales person name")]
    public string SalesPersonName { get; set; }
    [Description("Opportunitiy expected start date")]
    public DateTime? ExpectedStartDate { get; set; }
    [Description("Opportunitiy customer id")]
    public long? CustomerID { get; set; }
    [Description("Opportunitiy customer name")]
    public string CustomerName { get; set; }
    [Description("Opportunitiy customer email. This is used to resolve customer email in send contract tool.")]
    public string CustomerEmail { get; set; }
    [Description("Opportunitiy state name")]
    public string StateName { get; set; }
    [Description("Opportunitiy current price")]
    public decimal? CurrentPrice { get; set; }
    [Description("Opportunitiy expected closure date")]
    public DateTime? ExpectedClosure { get; set; }
    [Description("Opportunitiy tax exempt")]
    public short? TaxExempt { get; set; }
    [Description("Opportunitiy billing method")]
    public string BillingMethod { get; set; }
    [Description("Opportunitiy product ID")]
    public long? ProductID { get; set; }
    [Description("Opportunitiy service type name")]
    public string ServiceTypeName { get; set; }
    [Description("Opportunitiy custom pricing data")]
    public IEnumerable<CustomPricingDataMcp> CustomPricing { get; set; }
}

public sealed record CustomPricingDataMcp
{
    [Description("Opportunitiy custom pricing reference code")]
    public required string ReferenceCode { get; init; }
    [Description("Opportunitiy custom pricing ID")]
    public long ID { get; init; }
    [Description("Opportunitiy custom pricing level entity ID")]
    public long? LevelEntityID { get; init; }
    [Description("Opportunitiy custom pricing external ID")]
    public Guid ExternalID { get; init; }
    [Description("Opportunitiy custom pricing scenario data")]
    public IEnumerable<CustomPricingScenarioDataMcp> CustomPricingScenarios { get; init; }
    [Description("Indicates whether a contract can be created for this custom pricing in the opportunity. True = allowed; false = not allowed.")]
    public bool CanCreateContract { get; set; }
}

public sealed record CustomPricingScenarioDataMcp
{
    [Description("Custom pricing scenario ID")]
    public long ID { get; init; }
    [Description("Custom pricing scenario external ID")]
    public Guid ExternalID { get; init; }
    [Description("Custom pricing scenario term")]
    public short Term { get; init; }
    [Description("Custom pricing scenario price")]
    public decimal? Price { get; init; }
}