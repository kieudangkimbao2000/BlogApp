namespace BlogApp.Interfaces;

using BlogApp.Entities;

public interface IAccountRepository
{
    Account? GetAccountByUsername(string username);
    bool AddAccount(Account account);
}