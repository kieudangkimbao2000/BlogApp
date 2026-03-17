namespace BlogApp.Services;

using BlogApp.Handlers;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using System.Text;
using BlogApp.Entities;
using Npgsql.Internal;
using BlogApp.Common;

/// <summary>
///   Implement Authentication Service Interface
/// </summary>
/// <param name="repository"></param>
/// <param name="tokenHandler"></param>
public class AuthenService (IAccountRepository repository, 
                            TokenHandler tokenHandler): IAuthenService
{
    public RespDTO LoginUser(LoginDTO login)
    {
        var user = repository.GetAccountByUsername(login.Username);

        if (user != null && 
            PasswordHandler
                .VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            string jwt = tokenHandler.CreateToken(user.ToDTO());

            return new LoginRespDTO(jwt, 200, "");
        }

        return new RespDTO(400, AppMessages.E0001);
    }

    public RespDTO RegisterUser(RegisterDTO register)
    {
        bool result = false;
        var existingAccount = repository.GetAccountByUsername(register.Username);

        if (existingAccount != null)
        {
            return new RespDTO(400, AppMessages.E0002);
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
            Description = register.Description,
            Avatar = " ",
            Role = "2",
            State = "1"
        };

        result = repository.AddAccount(account);

        if (!result)
        {
           return new RespDTO(500, AppMessages.E0003);
        }

        return new RegisterRespDTO(account.ToDTO(), 200, "");
    }
}