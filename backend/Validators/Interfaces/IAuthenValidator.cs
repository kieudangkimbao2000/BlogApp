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
    bool ValidateRegisterRequest(RegisterDTO register);
}
