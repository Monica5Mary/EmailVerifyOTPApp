using EmailVerifyOTP.Application.Exceptions;
using EmailVerifyOTP.Application.Interfaces;
using EmailVerifyOTP.Application.Model;
using EmailVerifyOTP.Application.Services;
using FakeItEasy;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailVerifyOTP.UnitTestCase
{
    public class EmailServiceTest
    {
        private readonly ISendEmailService _sendEmailService;
        private readonly IEmailDetailsRepository _emailDetailrepo;
        private readonly IOptions<EmailAppConfig> _config;
        private readonly EmailService _emailService;

        public EmailServiceTest()
        {
            _sendEmailService = A.Fake<ISendEmailService>();
            _emailDetailrepo = A.Fake<IEmailDetailsRepository>();
            _config = A.Fake<IOptions<EmailAppConfig>>();
            //_emailService = new EmailService(_sendEmailService, _emailDetailrepo, _config);
        }

        [Fact]
        public async Task GenerateOtp_Positive_WhenEmailIsEntered()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.UpdatedDateTime.AddSeconds(100);
            var actualEmailData = CreateEmailData(actualUserData.EmailAddress, actualUserData.OtpCode);

            //Act
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).Returns(actualUserData);
            A.CallTo(() => _emailDetailrepo.CreateEmailDetail(actualUserData)).Returns(true);
            A.CallTo(() => _sendEmailService.SendEmail(actualEmailData)).Returns(true);
            var result = await _emailService.GenerateOtpForVerify(actualUserData.EmailAddress);

            //Assert
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailDetailrepo.CreateEmailDetail(actualUserData)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _sendEmailService.SendEmail(actualEmailData)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GenerateOtp_Negative_WhenEmailIsEnteredButDisabledAsOtpGenerated()
        {
            //Arrange
            var actualUserData = CreateUserDetail();

            //Act
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).Returns(actualUserData);
            Action act = async () => await _emailService.GenerateOtpForVerify(actualUserData.EmailAddress);

            //Assert
            OTPGeneratedException exception = Assert.Throws<OTPGeneratedException>(act);
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).MustHaveHappenedOnceExactly();
            Assert.Equal("Otp already generated, please try after 1 minute", exception.Message);
        }

        [Fact]
        public async Task ValidateOTP_Positive_WhenCorrectOtpEntered()
        {
            //Arrange
            var actualUserData = CreateUserDetail();

            //Act
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).Returns(actualUserData);
            var result = await _emailService.ValidateOTP(actualUserData.EmailAddress, actualUserData.OtpCode.ToString());

            //Assert
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).MustHaveHappenedOnceExactly();
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateOTP_Negative_WhenOtpTryCountMorethan10()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.RetryCount = 11;
            var otp = 123456;

            //Act
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).Returns(actualUserData);
            var result = await _emailService.ValidateOTP(actualUserData.EmailAddress, otp.ToString());

            //Assert
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).MustHaveHappenedOnceExactly();
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateOTP_Negative_WhenOtpIsTimeOut()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.UpdatedDateTime = DateTime.UtcNow.AddSeconds(100);

            //Act
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).Returns(actualUserData);
            Action act = async () => await _emailService.ValidateOTP(actualUserData.EmailAddress, actualUserData.OtpCode.ToString());

            //Assert
            A.CallTo(() => _emailDetailrepo.GetDetailByEmailAddress(actualUserData.EmailAddress)).MustHaveHappenedOnceExactly();
            TimeoutException exception = Assert.Throws<TimeoutException>(act);
            Assert.Equal("Timeout for entered Otp", exception.Message);
        }

      

        /// <summary>
        /// CreateUserDetail
        /// </summary>
        /// <returns></returns>
        private EmailUserDetails CreateUserDetail()
        {
            return new EmailUserDetails()
            {
                EmailAddress = "monica5mary@dso.org.sg",
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                ID = new Guid().ToString(),
                OtpCode = new Random().Next(0, 1000000),
                RetryCount = 0
            };
        }

        /// <summary>
        /// CreateEmailData
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        private EmailData CreateEmailData(string email, int? otp)
        {
            return new EmailData()
            {
                FromEmailID = _config.Value.FromEmail,
                ToEmailID = email,
                Body = $"Your OTP Code is {otp}. The code is valid for 1 minute",
                Subject = $"OTP For Email Verification"
            };
        }
    }
}
