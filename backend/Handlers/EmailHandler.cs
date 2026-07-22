using System.Net.Mail;

namespace BlogApp.Handlers;

/// <summary>
///     Handle sending email
/// </summary>
/// <param name="logger"></param>
public class EmailHandler(ILogger<EmailHandler> logger, IConfiguration config)
{
    /// <summary>
    ///     Send email to user
    /// </summary>
    /// <param name="to">Recipient email address</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body</param>
    /// <returns>True if email was sent successfully, otherwise false</returns>
    public bool SendEmail(string to, string subject, string body)
    {
        try
        {
            var host = config["EmailSettings:SmtpServer"];
            var senderEmail = config["EmailSettings:SenderEmail"];
            var senderPassword = config["EmailSettings:SenderPassword"];
            var port = int.Parse(config["EmailSettings:SmtpPort"]!);

            var client = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };
            client.Send(senderEmail, to, subject, body);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending email to {To} with subject {Subject}", to, subject);
            return false;
        }
    }
}