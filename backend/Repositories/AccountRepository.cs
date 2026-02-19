namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Interfaces;
using BlogApp.Entities;

public class AccountRepository(BlogAppContext context) : IAccountRepository
{
    public Account? GetAccountByUsername(string username)
    {
        return context.Accounts.FirstOrDefault(a => a.Username == username);
    }

    public bool AddAccount(Account account)
    {
        try
        {
        context.Accounts.Add(account);
        context.SaveChanges();

        return true;
        } catch(Exception ex)
        {
            return false;
        }
    }
}