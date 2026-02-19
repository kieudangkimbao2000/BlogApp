namespace BlogApp.Interfaces;

using BlogApp.DTOs;
using BlogApp.DTOs.Authentications;
    
public interface IAuthenService
{
   (string,string) LoginUser(LoginDTO login);

   (AccountDTO?,string) RegisterUser(RegisterDTO register);
}