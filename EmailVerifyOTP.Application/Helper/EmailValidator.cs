namespace EmailVerifyOTP.Application.Helper
{
    public static class EmailValidator
    {
        /// <summary>
        /// IsValidEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var trimEmail = email.Trim().ToString();
                var mail = new System.Net.Mail.MailAddress(email);
                if (mail.Address == trimEmail && trimEmail.Split('@')[1] == "dso.org.sg")
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
