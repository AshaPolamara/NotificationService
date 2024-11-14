namespace NotificationService
{
    public class OtpItem
    {
        public string Otp { get; set; }
        public DateTime Expiration { get; set; }
        public int Attempts { get; set; } = 0;
    }
}
