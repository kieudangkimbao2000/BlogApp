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
   RespDTO LoginUser(LoginDTO login);

   /// <summary>
   ///   Register a user
   /// </summary>
   /// <param name="register">Register info</param>
   /// <param name="errCode">Error code be returned</param>
   /// <returns>The registered account if successful, otherwise null</returns>
   RespDTO RegisterUser(RegisterDTO register);
}