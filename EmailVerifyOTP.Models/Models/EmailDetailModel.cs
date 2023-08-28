using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.DomainModels.Models
{
    [ExcludeFromCodeCoverage]
    public class EmailDetailModel : ParentEntity
    {
        public string EmailAddress { get; set; }
        public int? OtpCode { get; set; }
        public int RetryCount { get; set; }
    }
}
