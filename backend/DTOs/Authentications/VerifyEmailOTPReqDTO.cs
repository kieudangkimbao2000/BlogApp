namespace BlogApp.DTOs;

public class VerifyEmailOTPReqDTO
{
    public string SessionId { get; set; }
    public string OTP { get; set; }
}