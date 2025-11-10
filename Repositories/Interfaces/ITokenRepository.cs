using Microsoft.AspNetCore.Identity;

namespace RentEase.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
