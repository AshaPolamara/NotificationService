using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dto;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly EmailOtpService _emailOtpService;

        public OtpController(EmailOtpService emailOtpService)
        {
            _emailOtpService = emailOtpService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateOtp([FromBody] GenerateOtpRequest request)
        {
            var status = _emailOtpService.GenerateOtpEmail(request.Email);
            if (status == AppConstants.STATUS_EMAIL_OK)
            {
                return Ok("OTP has been sent to your email.");
            }

            return BadRequest(status.ToString());
        }

        [HttpPost("verify")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var status = _emailOtpService.CheckOtp(request.Email, request.Otp);
            if (status == AppConstants.STATUS_OTP_OK)
            {
                return Ok("OTP verified successfully.");
            }

            return BadRequest(status.ToString());
        }
    }
}
