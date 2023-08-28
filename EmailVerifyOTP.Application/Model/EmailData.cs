using System.Diagnostics.CodeAnalysis;

namespace EmailVerifyOTP.Application.Model
{
    [ExcludeFromCodeCoverage]
    public class EmailData
    {
            public string FromEmailID { get; set; }
            public string ToEmailID { get; set; }
            public string Body { get; set; }
            public string Subject { get; set; }
    }
}
