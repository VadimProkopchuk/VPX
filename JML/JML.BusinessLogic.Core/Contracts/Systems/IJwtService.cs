using JML.Domain;
using JML.Models;

namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface IJwtService
    {
        JwtModel GetToken(User user);
    }
}
