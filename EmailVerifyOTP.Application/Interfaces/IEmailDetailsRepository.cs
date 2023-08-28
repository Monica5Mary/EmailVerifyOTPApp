using EmailVerifyOTP.Application.Model;

namespace EmailVerifyOTP.Application.Interfaces
{
    public interface IEmailDetailsRepository
    {
        Task<bool> CreateEmailDetail(EmailUserDetails userDetail);
        Task<EmailUserDetails> GetDetailByEmailAddress(string emailAddr);
        Task<bool> UpdateEmailDetail(EmailUserDetails userDetail);

    }
}
