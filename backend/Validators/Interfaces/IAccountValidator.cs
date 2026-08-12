namespace BlogApp.Interfaces;

using BlogApp.DTOs;

/// <summary>
///   Account Validator
/// </summary>
public interface IAccountValidator
{
    /// <summary>
    ///   Validates the upload avatar request
    /// </summary>
    /// <param name="req">The upload avatar request DTO</param>
    /// <returns>True if request is valid, otherwise false</returns>
    bool ValidateUploadAvatarReq(RequestBaseDTO<UploadAvatarReqDTO> req);
}