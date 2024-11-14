"# NotificationService" 
This is Notification microservice application .

As this is to be used as enterprise level application . This module is created as asp dotnet core webapi .

Execution Steps :
1. Clone application into local 
2. Open in Visual Studio 
3. Run application .

Assumptions
Application: WebApi 
Cache : Using in-memory cache to preserve otp's and expiration times.
SMTP: A function SendEmail is provided that sends the email using SMTP.
Timer: 1-minute expiration on each OTP.
Retries: The module allows up to 10 attempts within the time limit.
Email Validation: Only emails from @dso.org.sg domain are allowed.

Swagger URL will be opened

It has 2 endpoints 
1. POST : /api/Otp/generate

2. POST : /api/otp/verify

Testing:

Generate OTP:
Send a POST request to /api/otp/generate with a valid email.
OTP will be generated and an email will be "sent" (simulated).
	Email should be xxxxx@dso.org.sg
	Email will be sent with OTP embedded in it.
	For Testing : Can see the OTP on console started when running the application . 
	If email is invalid : Bad request with STATUS_EMAIL_INVALID
	on Failure : STATUS_EMAIL_FAIL
	
Verify OTP:
Send a POST request to /api/otp/verify with the email and OTP printed on the console/email.
If the OTP is correct, the response will be STATUS_OTP_OK.
If the OTP is wrong, the response will indicate failure, and the number of attempts will be tracked.
If the OTP expires or the attempts exceed 10, it will return STATUS_OTP_TIMEOUT or STATUS_OTP_FAIL.
