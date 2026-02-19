namespace BlogApp.Handlers;

public class PasswordHandler
{
    /// <summary>
    /// Hash the password with a salt.
    /// </summary>
    /// <param name="password">password</param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();

        return BCrypt.Net.BCrypt.HashPassword(password , salt);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}