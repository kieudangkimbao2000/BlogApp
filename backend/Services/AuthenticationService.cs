namespace BlogApp.Services;

using BlogApp.Dbs;
using BlogApp.Handlers;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Models.Authentications;
using BlogApp.DIs;
using BlogApp.Models;
using System.Text;

public class AuthenticationService : IAuthenticationService
{
    private readonly BlogAppContext _context;

    public AuthenticationService(BlogAppContext context)
    {
        _context = context;
    }

    public (AccountDTO?,string) AuthenticateUser(Login login)
    {
        var user = _context.Accounts.FirstOrDefault(u => u.Username == login.Username);

        if (user != null && PasswordHandlers.VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            return (user.ToDTO(), "");
        }
        return (null, "Invalid username or password.");
    }

    public (AccountDTO?,string) RegisterUser(Register register)
    {
        var existingUser = _context.Accounts.FirstOrDefault(u => u.Username == register.Username);

        if (existingUser != null)
        {
            return (null, "Username already exists.");
        }

        var account = new Account
        {
            Username = register.Username,
            Password = Encoding.UTF8.GetBytes(PasswordHandlers.HashPassword(register.Password)),
            Name = register.Name,
            Address = register.Address,
            Phone = register.Phone,
            Email = register.Email,
            OtherContact = register.OtherContact,
            Description = register.Description
        };

        try
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();                                         
        }
        catch
        {
            return (null, "There was an error during registration!");
        }

        return (account.ToDTO(), "");
    }
}