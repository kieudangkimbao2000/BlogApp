namespace BlogApp.Interfaces;

using BlogApp.DTOs;

/// <summary>
///     Implement business logic related to accounts
/// </summary>
public interface IAccountService
{
    /// <summary>
    ///     Get all accounts
    /// </summary>
    /// <returns>List of all accounts</returns>
    List<AccountDTO> GetAllAccounts();

    /// <summary>
    ///     Get account by username
    /// </summary>
    /// <param name="username">username to be found</param>
    /// <param name="errCode">Error code be returned if a error occurs</param>
    /// <returns>An account if found, otherwise null</returns>
    AccountDTO? GetAccountByUsername(string username, ref string errCode);

    /// <summary>
    ///    Add a account
    /// </summary>
    /// <param name="accountDTO">Account data</param>
    /// <param name="errCode">Error code be returned if a error occurs</param>
    /// <returns>True if successful, otherwise false</returns>
    bool AddAccount(AccountDTO accountDTO, ref string errCode);

    /// <summary>
    ///     Update an account
    /// </summary>
    /// <param name="accountDTO">Account data</param>
    /// <param name="errCode">Error code be returned if a error occurs</param>
    /// <returns>True if successful, otherwise false</returns>
    bool UpdateAccount(AccountDTO accountDTO, ref string errCode);

    /// <summary>
    ///    Delete an account
    /// </summary>
    /// <param name="username"></param>
    /// <param name="errCode">Error code be returned if a error occurs</param>
    /// <returns>True if successful, otherwise false</returns>
    bool DeleteAccount(string username, ref string errCode);
}