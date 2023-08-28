using EmailVerifyOTP.Application.Interfaces;
using EmailVerifyOTP.Application.Model;

namespace EmailVerifyOTP.Infrastructures.Email
{
    public class SendEmailService: ISendEmailService
    {
        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<bool> SendEmail(EmailData emailAddress)
        {
            //This service will send email with Otp
            await Task.Delay(1000);
            return true;
        }
    }
}
