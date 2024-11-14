using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;

namespace NotificationService.Services
{
    public class EmailOtpService
    {
        private readonly IMemoryCache _memoryCache;

        private string generatedOtp;
        private int attempts;
        private bool otpValid;
        private bool isOtpSent;

        public EmailOtpService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Start() { }

        public void Close() { }

        public string GenerateOtpEmail(string userEmail)
        {
            if (!IsValidEmail(userEmail))
            {
                return AppConstants.STATUS_EMAIL_INVALID;
            }

            generatedOtp = GenerateOtp();
            var otpExpiration = DateTime.Now.AddMilliseconds(AppConstants.OTP_TIMEOUT);
            var cacheExpiration = DateTime.Now.AddMilliseconds(AppConstants.CACHE_TIMEOUT);
            // Store OTP in memory cache/ other datasource with expiration time
            _memoryCache.Set(userEmail, new OtpItem { Otp = generatedOtp, Expiration = otpExpiration , Attempts =0 }, cacheExpiration);
            attempts = 0;

            string emailBody = $"Your OTP Code is {generatedOtp}. The code is valid for 1 minute.";
            if (!SendEmail(userEmail, AppConstants.OTP_EMAIL_SUBJECT, emailBody))
            {
                return AppConstants.STATUS_EMAIL_FAIL;
            }

            return AppConstants.STATUS_EMAIL_OK;
        }

        public string CheckOtp(string userEmail, string inputOtp)
        {
            if (!_memoryCache.TryGetValue(userEmail, out OtpItem otpItem))
            {
                Console.WriteLine("No OTP has been generated yet.");
                return AppConstants.STATUS_OTP_FAIL;
            }

            if (DateTime.Now > otpItem.Expiration)
            {
                _memoryCache.Remove(userEmail); // Remove expired OTP from cache
                return AppConstants.STATUS_OTP_TIMEOUT;
            }

            if (otpItem.Attempts > AppConstants.MAX_ATTEMPTS)
            {
                return AppConstants.STATUS_OTP_FAIL;               
            }
            if (inputOtp == otpItem.Otp)
            {
                _memoryCache.Remove(userEmail);
                return AppConstants.STATUS_OTP_OK;
            }

            otpItem.Attempts++;
            _memoryCache.Set(userEmail, otpItem); //Set updated OTP attemts to useremail

            return AppConstants.STATUS_OTP_FAIL;
        }

        private string GenerateOtp()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] otpBytes = new byte[4];
                rng.GetBytes(otpBytes);
                int otpNumber = BitConverter.ToInt32(otpBytes, 0) % 1000000;
                return Math.Abs(otpNumber).ToString("D6");
            }
        }

        private bool SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}: {body}");
            return true;
        }

        private bool IsValidEmail(string email)
        {
            return email.EndsWith(AppConstants.EMAIL_DOMAIN);
        }
    }
}