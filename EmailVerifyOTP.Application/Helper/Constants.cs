namespace EmailVerifyOTP.Application.Helper
{
    public class Constants
    {
        //Action name
        public const string Generate_Const = "Generate";
        public const string Validate_Const = "Validate";

        //Description
        public const string GenerateAnOtpForAnEmail_Const = "Generate an OTP for an Email";
        public const string ValidateAnOtp_Const = "Validate entered OTP";

        //Validator
        public const string OtpIsEmpty_Const = "OTP is Required";
        public const string OtpIsNumeric_Const = "OTP should be Numeric Value";
        public const string EmailIsEmpty_Const = "Email address is required";
        public const string EmailIsInvalid_Const = "Invalid Email Address and it should be domain ('dso.org.sg')";

        //Error
        public const string InternalServerError_Const = "500 Internal server error";
    }
}
