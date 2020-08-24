using System;

namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface ISystemTimeService
    {
        DateTime GetDateUtc();
    }
}
