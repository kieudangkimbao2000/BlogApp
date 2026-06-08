namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Interfaces;
using BlogApp.Entities;

/// <summary>
///     Implement Account Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class AccountRepository(BlogAppContext context,
                                 ILogger<AccountRepository> logger) : IAccountRepository
{
    public List<Account> GetAllAccounts()
    {
        return context.Accounts.OrderByDescending(a => a.CreatedAt).ToList();
    }

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
            logger.LogError(ex, "Error adding account with username {Username}", 
                            account.Username);
            return false;
        }
    }

    public bool UpdateAccount(Account account)
    {
        try
        {
            context.Accounts.Update(account);
            context.SaveChanges();

            return true;
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error updating account with username {Username}", 
                            account.Username);
            return false;
        }
    }

    public bool DeleteAccount(Account account)
    {
        try
        {
            context.Accounts.Remove(account);
            context.SaveChanges();

            return true;
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting account with username {Username}",
                             account.Username);
            return false;
        }
    }
}