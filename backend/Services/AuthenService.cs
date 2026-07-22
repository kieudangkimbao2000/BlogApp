namespace BlogApp.Services;

using BlogApp.Handlers;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using System.Text;
using BlogApp.Entities;
using BlogApp.Common;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

/// <summary>
///   Implement Authentication Service Interface
/// </summary>
/// <param name="repository"></param>
/// <param name="tokenHandler"></param>
public class AuthenService (IAccountRepository repository, 
                            TokenHandler tokenHandler,
                            IAuthenValidator validator,
                            EmailHandler emailHandler,
                            IDistributedCache cache): IAuthenService
{
    public ResponseBaseDTO LoginUser(LoginReqDTO login)
    {
        if (!validator.ValidateLoginRequest(login))
        {
            return new ResponseBaseDTO(400);
        }

        var user = repository.GetAccountByUsername(login.Username);

        if (user != null && PasswordHandler
                            .VerifyPassword(login.Password, Encoding.UTF8.GetString(user.Password)))
        {
            string jwt = tokenHandler.CreateToken(user.ToDTO());

            return new ResponseBaseDTO<LoginRespDTO>(new LoginRespDTO(jwt), 200);
        }

        return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0001), 400);
    }

    public ResponseBaseDTO RegisterUser(RegisterReqDTO register)
    {
        bool result = false;
        if (!validator.ValidateRegisterRequest(register))
        {
            return new ResponseBaseDTO(400);
        }
        

        var account = new Account
        {
            Username = register.Username,
            Password = Encoding.UTF8.GetBytes(PasswordHandler.HashPassword(register.Password)),
            FullName = register.FullName,
            Address = "",
            Phone = "",
            Email = register.Email,
            OtherContact = "",
            Description = "",
            Avatar = "",
            Role = "2",
            Functions = new string[]{},
            State = 1
        };

        result = repository.AddAccount(account);

        if (!result)
        {
           return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0003), 500);
        }

        string jwt = tokenHandler.CreateToken(account.ToDTO());

        return new ResponseBaseDTO<LoginRespDTO>(new LoginRespDTO(jwt), 200);
    }

    public ResponseBaseDTO VerifyRegisterInfo(RegisterReqDTO register)
    {
        if (!validator.ValidateRegisterRequest(register))
        {
            return new ResponseBaseDTO(400);
        }

        var account = repository.GetAccountByUsername(register.Username);

        if (account != null)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0002), 400);
        }

        if (repository.IsEmailExist(register.Email))
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0005), 400);
        }

        return new ResponseBaseDTO(200);
    }

    public ResponseBaseDTO AuthenticateEmail(AuthenEmailReqDTO authenEmail)
    {
        string subject = "[No reply]Authenticatate email for BlogApp";
        string body = $"We received a request to register account with this email. \n" + 
                        $"If you did not make this request, please ignore this email. \n" + 
                        "This is OTP code for your email authentication: {0}";

        var random = new Random();
        var otp = random.Next(100000, 999999).ToString();

        body = string.Format(body, otp);

        emailHandler.SendEmail(authenEmail.Email, subject, body);

        var otpHash = PasswordHandler.HashPassword(otp);
        var sessionId = Guid.NewGuid().ToString();

        try
        {
            cache.SetString(sessionId + "_otpHash", otpHash, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            });
        }
        catch (RedisException)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0006), 503);
        }
        
        return new ResponseBaseDTO<AuthenEmailRespDTO>(new AuthenEmailRespDTO { SessionId = sessionId }, 200);
    }

    public ResponseBaseDTO VerifyEmailOTP(VerifyEmailOTPReqDTO verifyEmail)
    {
        var otp = verifyEmail.OTP;
        var sessionId = verifyEmail.SessionId;
        string? otpHash;

        try
        {
            otpHash = cache.GetString(sessionId + "_otpHash");
        }
        catch (RedisException)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0006), 503);
        }

        if (string.IsNullOrEmpty(otpHash))
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0004), 400);
        }

        if (PasswordHandler.VerifyPassword(otp, otpHash))
        {
            return new ResponseBaseDTO(200);
        }

        return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E0004), 400);
    }
}