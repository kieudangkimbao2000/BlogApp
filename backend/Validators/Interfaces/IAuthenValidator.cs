namespace BlogApp.Interfaces;

using BlogApp.DTOs;

/// <summary>
///   Authentication Validator
/// </summary>
public interface IAuthenValidator
{
    /// <summary>
    ///   Validates the login request
    /// </summary>
    /// <param name="login">The login request DTO</param>
    /// <returns>True if the login request is valid, otherwise false</returns>
    bool ValidateLoginRequest(LoginReqDTO login);

    /// <summary>
    ///   Validates the register request
    /// </summary>
    /// <param name="register">The register request DTO</param>
    /// <returns>True if the register request is valid, otherwise false</returns>
    bool ValidateRegisterRequest(RegisterReqDTO register);
    
    /// <summary>
    ///   Validates before registering a user
    /// </summary>
    /// <param name="register">The register request DTO</param>
    /// <returns>True if the user can be registered, otherwise false</returns>
    bool ValidateBeforeRegisterUser(RegisterReqDTO register);

    /// <summary>
    ///  Validates the email authentication request
    /// </summary>
    /// <param name="req">The email authentication request DTO</param>
    /// <returns>True if the email authentication request is valid, otherwise false</returns>
    bool ValidateAuthenticateEmail(AuthenEmailReqDTO req);
}
