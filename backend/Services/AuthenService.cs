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
                            TokenHandler tokenHandler,
                            IAuthenValidator validator): IAuthenService
{
    public ResponseBaseDTO LoginUser(LoginReqDTO login)
    {
        if (!validator.ValidateLoginRequest(login))
        {
            return new ResponseBaseDTO(400);
        }

        var user = repository.GetAccountByUsername(login.Username);

        if (user != null && PasswordHandler
                            .VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            string jwt = tokenHandler.CreateToken(user.ToDTO());

            return new ResponseBaseDTO<LoginRespDTO>(new LoginRespDTO(jwt), 200);
        }

        return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0001), 400);
    }

    public ResponseBaseDTO RegisterUser(RegisterDTO register)
    {
        bool result = false;
        var exsAccount = repository.GetAccountByUsername(register.Username);

        if (exsAccount != null)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0002), 400);
        }

        var account = new Account
        {
            Username = register.Username,
            Password = Encoding.UTF8.GetBytes(PasswordHandler.HashPassword(register.Password)),
            FullName = register.FullName,
            Address = register.Address,
            Phone = register.Phone,
            Email = register.Email,
            OtherContact = register.OtherContact,
            Description = register.Description,
            Avatar = "",
            Role = "2",
            Functions = [],
            State = 1
        };

        result = repository.AddAccount(account);

        if (!result)
        {
           return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0003), 500);
        }

        return new ResponseBaseDTO<AccountDTO>(account.ToDTO(), 200);
    }
}