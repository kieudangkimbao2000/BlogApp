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
    /// <returns>An account if found, otherwise null</returns>
    AccountDTO? GetAccountByUsername(string username);

    /// <summary>
    ///    Add a account
    /// </summary>
    /// <param name="accountDTO">Account data</param>
    /// <returns>True if successful, otherwise false</returns>
    bool AddAccount(AccountDTO accountDTO);

    /// <summary>
    ///     Update an account
    /// </summary>
    /// <param name="accountDTO">Account data</param>
    /// <returns>True if successful, otherwise false</returns>
    bool UpdateAccount(AccountDTO accountDTO);

    /// <summary>
    ///    Delete an account
    /// </summary>
    /// <param name="username"></param>
    /// <returns>True if successful, otherwise false</returns>
    bool DeleteAccount(string username);
}