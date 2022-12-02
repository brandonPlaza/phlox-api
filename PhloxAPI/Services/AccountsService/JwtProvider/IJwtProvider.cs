using PhloxAPI.Models;

namespace PhloxAPI.Services.AccountsService.JwtProvider
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
