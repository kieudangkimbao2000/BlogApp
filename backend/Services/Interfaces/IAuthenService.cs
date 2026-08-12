namespace BlogApp.Interfaces;

using BlogApp.DTOs;

/// <summary>
///   Business logic related to authentication
/// </summary>
public interface IAuthenService
{
   /// <summary>
   ///   Login a user
   /// </summary>
   /// <param name="login">Login info</param>
   /// <param name="errCode">Error code be returned</param>
   /// <returns>A token if successful, otherwise an empty string</returns>
   ResponseBaseDTO LoginUser(LoginReqDTO login);

   /// <summary>
   ///   Register a user
   /// </summary>
   /// <param name="register">Register info</param>
   /// <param name="errCode">Error code be returned</param>
   /// <returns>The registered account if successful, otherwise null</returns>
   ResponseBaseDTO RegisterUser(RegisterReqDTO register);

   /// <summary>
   ///   Verify register info
   /// </summary>
   /// <param name="register">Register info</param>
   /// <returns>Status Code</returns>
   ResponseBaseDTO VerifyRegisterInfo(RegisterReqDTO register);

   /// <summary>
   ///  Authenticate an email
   /// </summary>
   /// <param name="authEmail">Authentication email request</param>
   /// <returns>Status Code and Session ID</returns>
   ResponseBaseDTO AuthenticateEmail(AuthenEmailReqDTO authEmail);

   /// <summary>
   ///   Verify email OTP
   /// </summary>
   /// <param name="verifyEmail">Request containing OTP sent to the email and session id</param>
   /// <returns>Status Code</returns>
   ResponseBaseDTO VerifyEmailOTP(VerifyEmailOTPReqDTO verifyEmail);

   /// <summary>
   ///  Change password for a user
   /// </summary>
   /// <param name="changePassword">Request containing old and new passwords</param>
   /// <returns>Status Code</returns>
   ResponseBaseDTO ChangePassword(ChangePasswordReqDTO changePassword);
}