using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PPMcpDemo.Models
{
    /// <summary>
    /// Represents filters for querying opportunities with cursor-based pagination.
    /// </summary>
    public class VMOpportunityFilter
    {
        [Description("Filters by opportunity item name (supports partial matching).")]
        public string Name { get; set; }

        [Description("Filters by one or more customer name (supports partial matching).")]
        public List<string> CustomerName { get; set; } = new();

        [Description("Filters by one or more salesperson name (supports partial matching).")]
        public List<string> SalesPerson { get; set; } = new();

        [Description("Filters by one or more external IDs (exact matches).")]
        public List<Guid> ExternalID { get; set; } = new();

        [Description("Filters by one or more status names (exact matches).")]
        public List<string> Status { get; set; } = new();

        [Description("Filters opportunities by expected start date (>=).")]
        public DateTime? ExpectedStartDate { get; set; }

        [Description("Filters opportunities created on or after this date.")]
        public DateTime? CreatedDateFrom { get; set; }

        [Description("Filters opportunities created on or before this date.")]
        public DateTime? CreatedDateTo { get; set; }

        [Description("Filters by one or more service types (supports partial matching).")]
        public List<string> ServiceType { get; set; } = new();

        [Description("Cursor for fetching the next page of results. Base64 encoded ID. If null there are no more result, if not null should request for next page")]
        public string NextCursor { get; set; }
    }
}
