namespace EmailVerifyOTP.Application.Exceptions
{
        public class OTPGeneratedException : Exception
        {
            public OTPGeneratedException()
            {

            }

            public OTPGeneratedException(string message)
                : base(message)
            {

            }
        }
}
