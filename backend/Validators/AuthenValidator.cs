namespace BlogApp.Validators;

using BlogApp.DTOs;
using BlogApp.Entities;
using BlogApp.Interfaces;

public class AuthenValidator(IAccountRepository repository) : IAuthenValidator
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

    public bool ValidateRegisterRequest(RegisterReqDTO register)
    {
        //Validate not null
        if (register == null)
        {
            return false;
        }

        // Validate required fields
        if(string.IsNullOrWhiteSpace(register.Username) || string.IsNullOrWhiteSpace(register.Password) || 
                    string.IsNullOrWhiteSpace(register.Email) || string.IsNullOrWhiteSpace(register.FullName))
        {
            return false;
        }
        
        // Validate email format
        string regexMail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if(!System.Text.RegularExpressions.Regex.IsMatch(register.Email, regexMail))
        {
            return false;
        }

        return true;
    }

    public bool ValidateBeforeRegisterUser(RegisterReqDTO register)
    {
        // Validate if the username already exists
        var user = repository.GetAccountByUsername(register.Username);
        if (user != null)
        {
            return false;
        }

        // Validate if the email already exists
        var userByEmail = repository.GetAccountByEmail(register.Email);
        if (userByEmail != null)
        {
            return false;
        }

        return true;
    }

    public bool ValidateAuthenticateEmail(AuthenEmailReqDTO req)
    {
        // Validate not null
        if (req == null)
        {
            return false;
        }

        // Validate required fields
        if(string.IsNullOrWhiteSpace(req.Email))
        {
            return false;
        }

        // Validate email format
        string regexMail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if(!System.Text.RegularExpressions.Regex.IsMatch(req.Email, regexMail))
        {
            return false;
        }

        return true;
    }
}