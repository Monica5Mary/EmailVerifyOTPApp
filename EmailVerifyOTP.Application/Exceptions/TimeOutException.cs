namespace EmailVerifyOTP.Application.Exceptions
{
    public class TimeOutException : Exception
    {
        public TimeOutException()
        {

        }

        public TimeOutException(string message)
            : base(message)
        {

        }
    }
}
