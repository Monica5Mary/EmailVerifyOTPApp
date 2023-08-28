using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.Application.Model
{
    [ExcludeFromCodeCoverage]
    public class GenerateOTPDto
    {
            public string EmailAddress { get; set; }
            public string OtpCode { get; set; }
    }
}
