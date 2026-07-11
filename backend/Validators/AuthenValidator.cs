namespace BlogApp.Validators;

using BlogApp.DTOs;
using BlogApp.Interfaces;

public class AuthenValidator : IAuthenValidator
{    public bool ValidateLoginRequest(LoginReqDTO login)
    {
        // validate the login object is not null
        if (login == null)
        {
            return false;
        }

        // Validate required fields 
        if(string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
        {
            return false;
        }

        return true;
    }

    public bool ValidateRegisterRequest(RegisterDTO register)
    {
        return true;
    }
}