using System;

namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface ISystemTimeService
    {
        DateTime GetDateUtc();
    }
}
