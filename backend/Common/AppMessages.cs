namespace BlogApp.Common;

/// <summary>
///   Common messages used in the application
/// </summary>
public static class AppMessages
{
    /*---Information Message---*/

    // Information messages related to authentication
    public const string I0001 = "User registered successfully!";
    public const string I0002 = "User logged in successfully!";

    // Information messages related to blog
    public const string I1001 = "Blog was added successfully!";
    public const string I1002 = "Blog was updated successfully!";
    public const string I1003 = "Blog was deleted successfully!";

    // Information messages related to account
    public const string I2001 = "Account was added successfully!";
    public const string I2002 = "Account was updated successfully!";
    public const string I2003 = "Account was deleted successfully!";

    // Information messages related to category
    public const string I3001 = "Category was added successfully!";
    public const string I3002 = "Category was updated successfully!";
    public const string I3003 = "Category was deleted successfully!";

    // Information messages related to comment
    public const string I4001 = "Comment was added successfully!";
    public const string I4002 = "Comment was updated successfully!";
    public const string I4003 = "Comment was deleted successfully!";

    /*---Eror Message---*/

    // Error messages related to authentication
    public const string E0001 = "Username or password is incorrect!";
    public const string E0002 = "Username already exists!";
    public const string E0003 = "Error occurred while registering user!";

    // Error messages related to blog
    public const string E1001 = "Blog not found!";
    public const string E1002 = "Blog already exists!";
    public const string E1003 = "Error occurred while creating blog!";
    public const string E1004 = "Error occurred while updating blog!";
    public const string E1005 = "Error occurred while deleting blog!";

    // Error messages related to account
    public const string E2001 = "Account not found!";
    public const string E2002 = "Account already exists!";
    public const string E2003 = "Error occurred while adding account!";
    public const string E2004 = "Error occurred while updating account!";
    public const string E2005 = "Error occurred while deleting account!";

    // Error messages related to category
    public const string E3001 = "Category not found!";
    public const string E3002 = "Category already exists!";
    public const string E3003 = "Error occurred while adding category!";
    public const string E3004 = "Error occurred while updating category!";
    public const string E3005 = "Error occurred while deleting category!";

    // Error messages related to comment
    public const string E4001 = "Comment not found!";
    public const string E4002 = "Error occurred while adding comment!";
    public const string E4003 = "Error occurred while updating comment!";
    public const string E4004 = "Error occurred while deleting comment!";

    /// <summary>
    ///     Get message by message code
    /// </summary>
    /// <param name="msgCode"></param>
    /// <returns></returns>
    public static string GetMessage(string msgCode)
    {
        return msgCode switch
        {
            //Information messages
            I0001 => I0001,
            I0002 => I0002,
            I1001 => I1001,
            I1002 => I1002,
            I1003 => I1003,
            I2001 => I2001,
            I2002 => I2002,
            I2003 => I2003,
            I3001 => I3001,
            I3002 => I3002,
            I3003 => I3003,
            I4001 => I4001,
            I4002 => I4002,
            I4003 => I4003,
            //Error messages
            E0001 => E0001,
            E0002 => E0002,
            E0003 => E0003,
            E1001 => E1001,
            E1002 => E1002,
            E1003 => E1003,
            E1004 => E1004,
            E1005 => E1005,
            E2001 => E2001,
            E2002 => E2002,
            E2003 => E2003,
            E2004 => E2004,
            E2005 => E2005,
            E3001 => E3001,
            E3002 => E3002,
            E3003 => E3003,
            E3004 => E3004,
            E3005 => E3005,
            E4001 => E4001,
            E4002 => E4002,
            E4003 => E4003,
            E4004 => E4004,
            _ => "An unknown error occurred!"
        };
    }
}