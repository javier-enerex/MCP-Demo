using System.ComponentModel;

namespace PPMcpDemo.Models
{
    public class VMContract
    {
        [Description("Contract ID")]
        public long ContractID { get; set; }

        [Description("Opportunity ID")]
        public long OpportunityID { get; set; }

        [Description("Opportunity name")]
        public string OpportunityName { get; set; }
        [Description("Contract name")]
        public string ContractName { get; set; }

        [Description("Customer name")]
        public string CustomerName { get; set; }

        [Description("Sales person name")]
        public string SalesPersonName { get; set; }

        [Description("Customer External ID")]
        public Guid? CustomerExternalID { get; set; }

        [Description("Opportunity expected start date")]
        public DateTime? ExpectedStartDate { get; set; }

        [Description("Customer signed on date")]
        public DateTime? CustomerSignedOn { get; set; }

        [Description("Customer signed by")]
        public string CustomerSignedBy { get; set; }

        [Description("Uploaded by user")]
        public string UploadedBy { get; set; }

        [Description("Uploaded on date")]
        public DateTime? UploadedOn { get; set; }

        [Description("Contract sign date")]
        public DateTime? SignDate { get; set; }

        [Description("Contract status")]
        public string ContractStatus { get; set; }

        [Description("Service type")]
        public string ServiceType { get; set; }

        [Description("Contract start date")]
        public DateTime? StartDate { get; set; }
    }
}