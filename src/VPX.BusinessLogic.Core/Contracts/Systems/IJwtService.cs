using VPX.Domain;
using VPX.Models;

namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface IJwtService
    {
        JwtModel GetToken(User user);
    }
}
