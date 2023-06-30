using SocialAppApi.Entities;

namespace SocialAppApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
