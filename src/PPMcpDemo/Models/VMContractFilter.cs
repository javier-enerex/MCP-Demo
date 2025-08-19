using System.ComponentModel;

namespace PPMcpDemo.Models
{
    /// <summary>
    /// Represents filters for querying contracts with cursor-based pagination.
    /// </summary>
    public class VMContractFilter
    {
        [Description("Filters by one or more salesperson.")]
        public List<string> SalesPerson { get; set; } = new();
        [Description("Filters by one or more customer names.")]
        public List<string> CustomerName { get; set; } = new();

        //[Description("Filters by the signature method identifier.")]
        //public short? SignatureMethod { get; set; }

        [Description("Filters by the contract sign date on or after this date.")]
        public DateTime? SignDateIni { get; set; }

        [Description("Filters by the contract sign date on or before this date.")]
        public DateTime? SignDateEnd { get; set; }
        [Description("Filters by the contract created date on or after this date.")]
        public DateTime? CreatedDateIni { get; set; }

        [Description("Filters by the contract created date on or before this date.")]
        public DateTime? CreatedDateEnd { get; set; }

        [Description("Filters by one or more contract status.")]
        public List<string> Status { get; set; } = new();

        [Description("Filters by one or more service type.")]
        public List<string> ServiceType { get; set; } = new();

        [Description("Filters by a partial or full opportunity name.")]
        public string Name { get; set; }

        [Description("Cursor for retrieving the next page of results.")]
        public string NextCursor { get; set; }
    }
}
