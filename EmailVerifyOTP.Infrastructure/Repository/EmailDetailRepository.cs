using static EmailVerifyOTP.Infrastructure.Repository.EmailDetailRepository;
using EmailVerifyOTP.Application.Model;
using EmailVerifyOTP.Application.Interfaces;
//using EmailVerifyOTPApp.Infrastructure.Interfaces;

namespace EmailVerifyOTP.Infrastructure.Repository
{
    public class EmailDetailRepository : IEmailDetailsRepository
    {
            private readonly IOtpGenerateDBContext _context;

            public EmailDetailRepository(IOtpGenerateDBContext context)
            {
                _context = context;
            }

            /// <summary>
            /// CreateDetail
            /// </summary>
            /// <param name="userDetail"></param>
            /// <returns></returns>
            public async Task<bool> CreateEmailDetail(EmailUserDetails emailDetail)
            {
                _context.EmailDetail.Add(emailDetail);
                await _context.SaveChangesAsync();
                return true;
            }

        /// <summary>
        /// GetEmailUserDetails
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<EmailUserDetails> GetDetailByEmailAddress(string emailId)
            {
                return _context.EmailDetail.FirstOrDefault(x => x.EmailAddress == emailId);
            }

        /// <summary>
        /// UpdateDetail
        /// </summary>
        /// <param name="emailDetail"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmailDetail(EmailUserDetails emailDetail)
            {
                _context.EmailDetail.Update(emailDetail);
                await _context.SaveChangesAsync();
                return true;
            }
        }
}
