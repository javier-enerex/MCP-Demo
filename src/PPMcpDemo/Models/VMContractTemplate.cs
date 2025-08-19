using System.ComponentModel;

namespace PPMcpDemo.Models
{
    public class VMContractTemplate
    {
        [Description("The contract template name.")]
        public string TemplateName { get; set; }
        [Description("The contract template ID. This is used to resolve the template ID in add contract tools.")]
        public long ID { get; set; }
        [Description("Indicates if the contract template is selected.")]
        public bool Selected { get; set; }
        [Description("Indicates the order of the contract template.")]
        public short OrderBy { get; set; }
    }
}