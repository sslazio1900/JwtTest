using ErrorOr;

namespace AuthService.Interfaces;

public interface IAuthService
{
    ErrorOr<string> AuthenticateUser(string user, string password);
}