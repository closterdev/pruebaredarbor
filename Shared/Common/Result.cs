using System.ComponentModel.DataAnnotations;

namespace Shared.Common
{
    public enum Result
    {
        [Display(Name = "Success")]
        Success,

        [Display(Name = "Error")]
        Error,

        [Display(Name = "NoRecords")]
        NoRecords
    }
}