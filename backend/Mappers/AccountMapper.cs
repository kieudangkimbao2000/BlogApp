    namespace BlogApp.Mappers;
    using BlogApp.Entities;
    using BlogApp.DTOs;

    public static class AccountMapper
    {
        public static AccountDTO ToDTO(this Account account)
        {
            return new AccountDTO
            {
                Username = account.Username,
                Name  = account.Name,
                Address = account.Address,
                Phone = account.Phone,
                Email = account.Email,
                OtherContact = account.OtherContact,
                Description = account.Description
            };
        }

        public static Account ToModel(this AccountDTO accountDTO)
        {
            return new Account
            {
                Username = accountDTO.Username,
                Name  = accountDTO.Name,
                Address = accountDTO.Address,
                Phone = accountDTO.Phone,
                Email = accountDTO.Email,
                OtherContact = accountDTO.OtherContact,
                Description = accountDTO.Description
            };
        }
    }