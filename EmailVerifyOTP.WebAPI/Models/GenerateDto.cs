using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class GenerateDto
    {
        public string EmailAddress { get; set; }
    }
}
