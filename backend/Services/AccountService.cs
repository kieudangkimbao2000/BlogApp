namespace BlogApp.Services;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using BlogApp.Mappers;

/// <summary>
///   Implement Account Service Interface
/// </summary>
/// <param name="repository"></param>
public class AccountService(IAccountRepository repository) : IAccountService
{
    public List<AccountDTO> GetAllAccounts()
    {
        var accounts = repository.GetAllAccounts();
        return accounts.ToDTOList();
    }

    public AccountDTO? GetAccountByUsername(string username, ref string errCode)
    {
        var account = repository.GetAccountByUsername(username);

        if(account == null)
        {
            errCode = "E2001"; // Account not found
            return null;
        }

        return account?.ToDTO();
    }

    public bool AddAccount(AccountDTO accountDTO, ref string errCode)
    {
        bool result = false;
        var account = repository.GetAccountByUsername(accountDTO.Username);

        if(account != null)
        {
            errCode = "E2002"; // Account already exists
            return false;
        }

        result = repository.AddAccount(accountDTO.ToModel());

        if(!result)
        {
            errCode = "E2003"; // Failed to add account
        }

        return result;
    }

    public bool UpdateAccount(AccountDTO accountDTO, ref string errCode)
    {
        bool result = false;
        var account = repository.GetAccountByUsername(accountDTO.Username);

        if(account == null)
        {
            errCode = "E2001"; // Account not found
            return false;
        }
        
        result = repository.UpdateAccount(accountDTO.ToModel());
        if(!result)
        {
            errCode = "E2004"; // Failed to update account
        }

        return result;
    }

    public bool DeleteAccount(string username, ref string errCode)
    {
        bool result = false;
        var account = repository.GetAccountByUsername(username);

        if(account == null)
        {
            errCode = "E2001"; // Account not found
            return false;
        }

        result = repository.DeleteAccount(account);
        if(!result)
        {
            errCode = "E2005"; // Failed to delete account
        }

        return result;
    }

    public AccountDTO? GetAccountByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public bool AddAccount(AccountDTO accountDTO)
    {
        throw new NotImplementedException();
    }

    public bool UpdateAccount(AccountDTO accountDTO)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAccount(string username)
    {
        throw new NotImplementedException();
    }
}