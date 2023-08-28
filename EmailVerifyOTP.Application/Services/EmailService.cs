using EmailVerifyOTP.Application.Exceptions;
using EmailVerifyOTP.Application.Interfaces;
using EmailVerifyOTP.Application.Model;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EmailVerifyOTP.Application.Services
{
    public class EmailService: IEmailService
    {
        private readonly ISendEmailService _sendMailService;
        private readonly IEmailDetailsRepository _EmailDetailsRepository;
        private readonly IOptions<EmailAppConfig> _appconfig;

        public EmailService(ISendEmailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        /// <summary>
        /// GenerateOtpForVerify
        /// </summary>
        ///  /// <param name="email"></param>
        /// <returns></returns>
        public async Task<GenerateOTPDto> GenerateOtpForVerify(string emailAddr)
        {
            EmailUserDetails emailDetails = new EmailUserDetails()
            {
                ID = new Guid().ToString(),
                EmailAddress = emailAddr,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                OtpCode = CreateRandomNumericOtp(),
                RetryCount = 0
            };

            var emailData = await _EmailDetailsRepository.GetDetailByEmailAddress(emailAddr);
            if (emailData != null)
            {
                if (emailData.UpdatedDateTime.AddSeconds(60) > DateTime.Now)
                {
                    throw new OTPGeneratedException("Otp already generated, please try after 1 minute");
                    //otp already generated
                }
                emailData.OtpCode = emailDetails.OtpCode;
                emailData.UpdatedDateTime = DateTime.Now;
                emailData.RetryCount = 0;
                await _EmailDetailsRepository.UpdateEmailDetail(emailData);
                await SendEmail(emailAddr, emailData.OtpCode.ToString());
            }
            else
            {
                await _EmailDetailsRepository.CreateEmailDetail(emailData);
                await SendEmail(emailAddr, emailData.OtpCode.ToString());
            }
            return CreateGenerateOTPResponse(emailAddr, emailData.OtpCode.ToString());
        }

        /// <summary>
        /// CreateRandomOtp
        /// </summary>
        /// <returns></returns>
        public int CreateRandomNumericOtp()
        {
            // declare array string to generate random string with combination of numbers
            //char[] charArr = "0123456789".ToCharArray();
            //string strrandom = string.Empty;
            //Random objran = new Random();
            //int noofcharacters = 6;
            //for (int i = 0; i < noofcharacters; i++)
            //{
            //    //It will not allow Repetation of Characters
            //    int pos = objran.Next(1, charArr.Length);
            //    if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
            //        strrandom += charArr.GetValue(pos);
            //    else
            //        i--;
            //}
            var generator = new Random();
            return generator.Next(0, 1000000);
            //return strrandom;
        }

        /// <summary>
        /// CreateGenerateOTPResponse
        /// </summary>
        /// <param name="emailAddr"></param>
        /// <param name="otpCode"></param>
        /// <returns></returns>

        private GenerateOTPDto CreateGenerateOTPResponse(string emailAddr, string otpCode)
        {
            GenerateOTPDto response = new GenerateOTPDto()
            {
                EmailAddress = emailAddr,
                OtpCode = otpCode
            };
            return response;
        }

        private async Task<bool> SendEmail(string email, string otp)
        {
            var emailData = new EmailData()
            {
                FromEmailID = _appconfig.Value.FromEmail,
                ToEmailID = email,
                Body = $"Your OTP Code is {otp}. The code is valid for 1 minute",
                Subject = $"OTP For Email Verification"
            };
            await _sendMailService.SendEmail(emailData);
            return true;
        }

        /// <summary>
        /// ValidateOTP
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        /// <exception cref="TimeOutException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<bool> ValidateOTP(string emailAddr, string otp)
        {
            var emailDetails = await _EmailDetailsRepository.GetDetailByEmailAddress(emailAddr);
            if (emailDetails != null)
            {
                if (emailDetails.RetryCount <= 10)
                {
                    //timeout check
                    if (emailDetails.UpdatedDateTime.AddSeconds(60) > DateTime.Now)
                    {
                        if (emailDetails.OtpCode.ToString() != otp.ToString())
                        {
                            emailDetails.RetryCount++;
                            await _EmailDetailsRepository.UpdateEmailDetail(emailDetails);
                            //Otp invalid
                            return false;
                        }
                        else
                        {
                            emailDetails.UpdatedDateTime = DateTime.Now;
                            emailDetails.RetryCount = 0;
                            await _EmailDetailsRepository.UpdateEmailDetail(emailDetails);
                        }
                        return true;
                    }
                    throw new TimeOutException("OTP Expired after 1 Minute Timeout");
                }

                if(emailDetails.EmailAddress!= null)
                {
                    Regex emailregex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                    bool isValidEmail = emailregex.IsMatch(emailDetails.EmailAddress);
                    if (isValidEmail)
                    {
                        if (emailDetails.EmailAddress.Contains("dso.org.sg"))
                        {
                            return true;
                        }
                        else
                        {
                            new NotFoundException("Email address is invalid");
                        }
                    }
                }
                return false;
            }
            throw new NotFoundException("Email address is required");
        }

    }
}
