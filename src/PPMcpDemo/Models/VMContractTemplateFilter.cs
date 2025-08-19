using System.ComponentModel;

namespace PPMcpDemo.Models
{
    /// <summary>
    /// Represents filters for querying contracts templates.
    /// </summary>
    public sealed class VMContractTemplateFilter
    {
        [Description("Filter by opportunity external ID. This is the external ID of the opportunity associated with the contract. This field is required.")]
        public Guid OpportunityExternalID { get; set; }

        [Description("Filter by custom pricing external ID. This is the external ID of the custom pricing associated with the opportunity.")]
        public Guid? CustomPricingExternalID { get; set; }

        [Description("Filter by custom pricing scenario external ID. This is the external ID of the custom pricing scenario associated with the custom pricing.")]
        public Guid? CustomPricingScenarioExternalID { get; set; }

        [Description("Filter by template name.")]
        public string? TemplateName { get; set; }

    }
}
