using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class ValidateDto
    {
        public string EmailAddress { get; set; }
        public string OtpCode { get; set; }
    }
}
