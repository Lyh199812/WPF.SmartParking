using System;
using Zhaoxi.SmartParking.Server.EFCore;

namespace Zhaoxi.SmartParking.Server.IDbConfig
{
    public interface IDbConfig
    {
        EFCoreContext GetDbContext();
    }
}
