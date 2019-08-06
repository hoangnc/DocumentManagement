using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Persistence.ComplexTypes
{
    [ComplexType]
    public class SplitInformation
    {
        public string SplitData { get; set; }
    }
}
