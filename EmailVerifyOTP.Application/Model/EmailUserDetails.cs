namespace EmailVerifyOTP.Application.Model
{
    public class EmailUserDetails
    {
        public string ID { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? OtpCode { get; set; }
        public int RetryCount { get; set; }
    }
}
