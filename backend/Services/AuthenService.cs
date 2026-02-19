namespace BlogApp.Services;

using BlogApp.Handlers;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.DTOs.Authentications;
using BlogApp.Interfaces;
using System.Text;
using BlogApp.Entities;

public class AuthenService (IAccountRepository repository, TokenHandler tokenHandler): IAuthenService
{
    public (string,string) LoginUser(LoginDTO login)
    {
        var user = repository.GetAccountByUsername(login.Username);

        if (user != null && PasswordHandler.VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            string token = tokenHandler.CreateToken(user.ToDTO());

            return (token, "");
        }
        return ("", "Invalid username or password.");
    }

    public (AccountDTO?,string) RegisterUser(RegisterDTO register)
    {
        bool isSuccess = false;
        var existingUser = repository.GetAccountByUsername(register.Username);

        if (existingUser != null)
        {
            return (null, "Username already exists.");
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

        isSuccess = repository.AddAccount(account);

        if (!isSuccess)
        {
            return (null, "Failed to create account. Please try again.");
        }

        return (account.ToDTO(), "");
    }
}