using EmailVerifyOTP.Application.Model;

namespace EmailVerifyOTP.Application.Interfaces
{
    public interface IEmailService
    {
            Task<GenerateOTPDto> GenerateOtpForVerify(string email);
            Task<bool> ValidateOTP(string email, string otp);
    }
}
