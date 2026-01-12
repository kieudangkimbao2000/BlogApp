using BlogApp.DTOs;
using BlogApp.Models;
using BlogApp.Models.Authentications;

namespace BlogApp.DIs;
    
    public interface IAuthenticationService
    {
       (AccountDTO?,string) AuthenticateUser(Login login);

       (AccountDTO?,string)RegisterUser(Register register);
    }