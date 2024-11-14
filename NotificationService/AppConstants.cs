namespace NotificationService
{
    public class AppConstants
    {
        public const int OTP_TIMEOUT = 60000; // 1 minute in milliseconds
        public const int CACHE_TIMEOUT = 600000;
        public const int MAX_ATTEMPTS = 10;

        public static readonly string EMAIL_DOMAIN = "@dso.org.sg";
        public static readonly string OTP_EMAIL_SUBJECT = "Your OTP Code";

        public static readonly string STATUS_EMAIL_OK = "STATUS_EMAIL_OK";
        public static readonly string STATUS_EMAIL_FAIL = "STATUS_EMAIL_FAIL";
        public static readonly string STATUS_EMAIL_INVALID = "STATUS_EMAIL_INVALID";
        public static readonly string STATUS_OTP_OK = "STATUS_EMAIL_OK";
        public static readonly string STATUS_OTP_FAIL = "STATUS_OTP_FAIL";
        public static readonly string STATUS_OTP_TIMEOUT = "STATUS_OTP_TIMEOUT";
    }
}
