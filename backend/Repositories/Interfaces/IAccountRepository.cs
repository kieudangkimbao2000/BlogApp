namespace BlogApp.Interfaces;

using BlogApp.Entities;

/// <summary>
///   Interact with Accounts in the database
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    ///    Get all accounts
    /// </summary>
    /// <returns>List of all accounts ordered by creation date descending</returns>
    List<Account> GetAllAccounts();

    /// <summary>
    ///     Get an account by username
    /// </summary>
    /// <param name="username">username to be found</param>
    /// <returns>The account if found, otherwise null</returns>
    Account? GetAccountByUsername(string username);

    /// <summary>
    ///     Add an account
    /// </summary>
    /// <param name="account">Account data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddAccount(Account account);

    /// <summary>
    ///    Update an account
    /// </summary>
    /// <param name="account">Account data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateAccount(Account account);

    /// <summary>
    ///   Delete an account from
    /// </summary>
    /// <param name="account">Account data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteAccount(Account account);
}