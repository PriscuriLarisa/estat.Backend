using System.IdentityModel.Tokens.Jwt;

namespace eStat.BLL.Interfaces
{
    public interface IJWT
    {
        string Generate(Guid id);
        JwtSecurityToken Verify(string jwt);
    }
}