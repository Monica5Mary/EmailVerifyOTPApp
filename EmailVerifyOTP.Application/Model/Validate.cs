using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.Application.Model
{
    [ExcludeFromCodeCoverage]
    public class Validate
    {
        public string EmailAddress { get; set; }
        public int OtpCode { get; set; }
    }
}
