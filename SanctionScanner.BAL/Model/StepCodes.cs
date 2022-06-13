using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SanctionScanner.BAL.Model
{
    public enum StepCodes
    {
        [Display(Name = "Awaiting approval")]
        [Description("Awaiting approval")]
        AwaitingApproval = 1,

        [Display(Name = "Approved")]
        [Description("Approved")]
        Approved,

        [Display(Name = "Rejected")]
        [Description("Rejected")]
        Rejected,

        [Display(Name = "Payment pending")]
        [Description("Payment pending")]
        PaymentPending,

        [Display(Name = "Payment completed")]
        [Description("Payment completed")]
        PaymentCompleted

    }
}
