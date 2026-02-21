namespace BlogApp.Services;

using BlogApp.Handlers;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.DTOs.Authentications;
using BlogApp.Interfaces;
using System.Text;
using BlogApp.Entities;

/// <summary>
///   Implement Authentication Service Interface
/// </summary>
/// <param name="repository"></param>
/// <param name="tokenHandler"></param>
public class AuthenService (IAccountRepository repository, 
                            TokenHandler tokenHandler): IAuthenService
{
    public string LoginUser(LoginDTO login, ref string errCode)
    {
        var user = repository.GetAccountByUsername(login.Username);

        if (user != null && 
            PasswordHandler
                .VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            string token = tokenHandler.CreateToken(user.ToDTO());

            return token;
        }
        
        errCode = "E0001"; // Invalid username or password

        return "";
    }

    public AccountDTO? RegisterUser(RegisterDTO register, ref string errCode)
    {
        bool result = false;
        var existingAccount = repository.GetAccountByUsername(register.Username);

        if (existingAccount != null)
        {
            errCode = "E0002"; // Username already exists
            return null;
        }

        var account = new Account
        {
            Username = register.Username,
            Password = Encoding.UTF8.GetBytes(PasswordHandler.HashPassword(register.Password)),
            Name = register.Name,
            Address = register.Address,
            Phone = register.Phone,
            Email = register.Email,
            OtherContact = register.OtherContact,
            Description = register.Description
        };

        result = repository.AddAccount(account);

        if (!result)
        {
            errCode = "E0003"; // Failed to create account
            return null;
        }

        return account.ToDTO();
    }
}