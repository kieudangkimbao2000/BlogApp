namespace BlogApp.Services;

using BlogApp.Dbs;
using BlogApp.Handlers;
using BlogApp.Models;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Models.Authentications;

public class LoginService
{
    private readonly BlogAppContext _context;

    public LoginService(BlogAppContext context)
    {
        _context = context;
    }

    public AccountDTO? AuthenticateUser(Login login)
    {
        var user = _context.Accounts.FirstOrDefault(u => u.Username == login.Username);

        if (user != null && PasswordHandlers.VerifyPassword(login.Password, Convert.ToBase64String(user.Password)))
        {
            return user.ToDTO();
        }

        return null;
    }
}