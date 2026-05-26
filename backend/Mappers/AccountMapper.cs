namespace BlogApp.Mappers;
using BlogApp.Entities;
using BlogApp.DTOs;

/// <summary>
///     Map between Account Entity and DTO
/// </summary>
public static class AccountMapper
{
    /// <summary>
    ///    Map Account Entity to Account DTO
    /// </summary>
    /// <param name="account">Account Entity Object</param>
    /// <returns>AccountDTO Object</returns>
    public static AccountDTO ToDTO(this Account account)
    {
        return new AccountDTO
        {
            Username = account.Username,
            FullName = account.FullName,
            Address = account.Address,
            Phone = account.Phone,
            Email = account.Email,
            OtherContact = account.OtherContact,
            Description = account.Description,
            Avatar = account.Avatar,
            Role = account.Role,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }

    /// <summary>
    ///     Map Account DTO to Account Entity
    /// </summary>
    /// <param name="accountDTO">Account DTO Object</param>
    /// <returns>Account Entity Object</returns>
    public static Account ToModel(this AccountDTO accountDTO)
    {
        return new Account
        {
            Username = accountDTO.Username,
            FullName = accountDTO.FullName,
            Address = accountDTO.Address,
            Phone = accountDTO.Phone,
            Email = accountDTO.Email,
            OtherContact = accountDTO.OtherContact,
            Description = accountDTO.Description,
            Avatar = accountDTO.Avatar,
            Role = accountDTO.Role,
        };
    }

    /// <summary>
    ///     Map List of Account Entity to List of Account DTO
    /// </summary>
    /// <param name="accounts">List of Account Entities</param>
    /// <returns>List of Account DTOs</returns>
    public static List<AccountDTO> ToDTOList(this List<Account> accounts)
    {
            return accounts.Select(a => a.ToDTO()).ToList();
    }
}