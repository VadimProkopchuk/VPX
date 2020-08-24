using System;

namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface IContextService
    {
        Guid? GetCurrentUserId();
    }
}
