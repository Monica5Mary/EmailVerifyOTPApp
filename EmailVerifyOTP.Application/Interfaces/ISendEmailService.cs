using EmailVerifyOTP.Application.Model;

namespace EmailVerifyOTP.Application.Interfaces
{
    public interface ISendEmailService
    {
            Task<bool> SendEmail(EmailData email);
    }
}
