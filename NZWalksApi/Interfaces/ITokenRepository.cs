using Microsoft.AspNetCore.Identity;

namespace ISWalksApi.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
