using System;

namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface IContextService
    {
        Guid? GetCurrentUserId();
    }
}
